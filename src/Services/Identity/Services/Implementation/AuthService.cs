namespace PFA.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;

    private readonly IApplicationService _applicationService;

    private readonly IScopeService _scopeService;

    private readonly HttpContext _context;

    private readonly IOpenIddictAuthorizationManager _authorizationManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(IHttpContextAccessor httpContextAccessor,
                                IUserService userService,
                                IApplicationService applicationService,
                                IScopeService scopeService,
                                IOpenIddictAuthorizationManager authorizationManager,
                                SignInManager<ApplicationUser> signInManager)
    {
        _context = httpContextAccessor.HttpContext;
        _userService = userService;
        _applicationService = applicationService;
        _scopeService = scopeService;
        _authorizationManager = authorizationManager;
        _signInManager = signInManager;
    }

    public async Task<AuthorizeDTO> AuthorizeAsync()
    {
        OpenIddictRequest request = GetOpenIddictServerRequest();

        AuthenticateResult result = await _context.AuthenticateAsync(IdentityConstants.ApplicationScheme);

        bool isRequestOrResultInvalid = result == null || !result.Succeeded || request.HasPrompt(Prompts.Login) ||
            (request.MaxAge != null && result.Properties?.IssuedUtc != null &&
                DateTimeOffset.UtcNow - result.Properties.IssuedUtc > TimeSpan.FromSeconds(request.MaxAge.Value));

        if (isRequestOrResultInvalid)
        {
            if (request.HasPrompt(Prompts.None))
            {
                return new AuthorizeDTO(Errors.LoginRequired, "The user is not logged in.");
            }
            else
            {
                return new AuthorizeDTO(GetRedirectUri(request));
            }
        }

        return await GetResultAsync(request, result);
    }

    public async Task<AuthorizeDTO> AcceptAsync()
    {
        OpenIddictRequest request = GetOpenIddictServerRequest();

        ApplicationUser user = await _userService.GetByClaimsPrincipalAsync(_context.User);

        object application = await _applicationService.FindByClientIdAsync(request.ClientId);

        string applicationId = await _applicationService.GetIdAsync(application);

        List<object> authorizations = await FindAuthorizations(user.Id, applicationId, request);

        bool hasConsentTypeAsync = await _applicationService.HasConsentTypeAsync(application, ConsentTypes.External);

        if (!authorizations.Any() && hasConsentTypeAsync)
        {
            return new AuthorizeDTO(Errors.ConsentRequired, "The logged in user is not allowed to access this client application.");
        }

        return await GetClaimsIdentity(user, request, authorizations, applicationId);
    }

    public async Task<AuthorizeDTO> SignOutAsync()
    {
        await _signInManager.SignOutAsync();

        return new AuthorizeDTO("/");
    }

    public async Task<AuthorizeDTO> ExchanegAsync()
    {
        OpenIddictRequest request = GetOpenIddictServerRequest();

        if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
        {
            AuthenticateResult authResult = await _context.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = authResult.Principal;

            ApplicationUser user = await _userService.FindByIdAsync(principal.GetClaim(Claims.Subject));

            if (user is null)
            {
                return new AuthorizeDTO(Errors.InvalidGrant, "The token is no longer valid.");
            }

            if (!await _signInManager.CanSignInAsync(user))
            {
                return new AuthorizeDTO(Errors.InvalidGrant, "The user is no longer allowed to sign in.");
            }

            principal.SetDestinations(GetDestinations);

            return new AuthorizeDTO(principal);
        }

        throw new InvalidOperationException("The specified grant type is not supported.");
    }

    #region Private Methods

    private OpenIddictRequest GetOpenIddictServerRequest()
    {
        OpenIddictRequest request = _context.GetOpenIddictServerRequest();

        if (request == null)
        {
            throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");
        }

        return request;
    }

    private string GetRedirectUri(OpenIddictRequest request)
    {
        string prompt = string.Join(" ", request.GetPrompts().Remove(Prompts.Login));

        HttpRequest httpRequest = _context.Request;

        List<KeyValuePair<string, StringValues>> parameters = httpRequest.HasFormContentType ?
            httpRequest.Form.Where(parameter => parameter.Key != Parameters.Prompt).ToList() :
            httpRequest.Query.Where(parameter => parameter.Key != Parameters.Prompt).ToList();

        parameters.Add(KeyValuePair.Create(Parameters.Prompt, new StringValues(prompt)));

        string redirectUri = httpRequest.PathBase + httpRequest.Path + QueryString.Create(parameters);

        return redirectUri;
    }

    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        switch (claim.Type)
        {
            case Claims.Name:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Profile))
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Email:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Email))
                    yield return Destinations.IdentityToken;

                yield break;

            case Claims.Role:
                yield return Destinations.AccessToken;

                if (claim.Subject.HasScope(Scopes.Roles))
                    yield return Destinations.IdentityToken;

                yield break;

            case "AspNet.Identity.SecurityStamp": yield break;

            default:
                yield return Destinations.AccessToken;
                yield break;
        }
    }

    private async Task<AuthorizeDTO> GetResultAsync(OpenIddictRequest request, AuthenticateResult result)
    {
        ApplicationUser user = await _userService.GetByClaimsPrincipalAsync(result.Principal);

        object application = await _applicationService.FindByClientIdAsync(request.ClientId);

        string applicationId = await _applicationService.GetIdAsync(application);

        List<object> authorizations = await FindAuthorizations(user.Id, applicationId, request);

        string consentType = await _applicationService.GetConsentTypeAsync(application);

        switch (consentType)
        {
            case ConsentTypes.External when !authorizations.Any():
                {
                    return new AuthorizeDTO(Errors.ConsentRequired, "The logged in user is not allowed to access this client application.");
                }
            case ConsentTypes.Implicit:
            case ConsentTypes.External when authorizations.Any():
            case ConsentTypes.Explicit when authorizations.Any() && !request.HasPrompt(Prompts.Consent):
                {
                    return await GetClaimsIdentity(user, request, authorizations, applicationId);
                }
            case ConsentTypes.Explicit when request.HasPrompt(Prompts.None):
            case ConsentTypes.Systematic when request.HasPrompt(Prompts.None):
                {
                    return new AuthorizeDTO(Errors.ConsentRequired, "Interactive user consent is required.");
                }
            default:
                return new AuthorizeDTO
                {
                    ViewModel = new()
                    {
                        ApplicationName = await _applicationService.GetLocalizedDisplayNameAsync(application),
                        Scope = request.Scope
                    }
                };
        }
    }

    private async Task<AuthorizeDTO> GetClaimsIdentity(ApplicationUser user, OpenIddictRequest request, List<object> authorizations, string applicationId)
    {
        ClaimsIdentity identity = new ClaimsIdentity(
                        authenticationType: TokenValidationParameters.DefaultAuthenticationType,
                        nameType: Claims.Name,
                        roleType: Claims.Role);

        ImmutableArray<string> roles = await _userService.GetRolesAsync(user);

        identity.AddClaim(Claims.Subject, user.Id)
                .AddClaim(Claims.Email, user.Email)
                .AddClaim(Claims.Name, user.UserName)
                .AddClaims(Claims.Role, roles);

        identity.SetScopes(request.GetScopes());

        List<string> resources = await _scopeService.ListResourcesAsync(identity.GetScopes());

        identity.SetResources(resources);

        var authorization = authorizations.LastOrDefault();
        if (authorization is null)
        {
            authorization = await _authorizationManager.CreateAsync(
                principal: new ClaimsPrincipal(identity),
                subject: user.Id,
                client: applicationId,
                type: AuthorizationTypes.Permanent,
                scopes: identity.GetScopes());
        }

        string authorizationId = await _authorizationManager.GetIdAsync(authorization);

        identity.SetAuthorizationId(authorizationId);
        identity.SetDestinations(GetDestinations);

        return new AuthorizeDTO(identity);
    }

    private async Task<List<object>> FindAuthorizations(string userId, string applicationId, OpenIddictRequest request)
    {
        return await _authorizationManager.FindAsync(
            subject: userId,
            client: applicationId,
            status: Statuses.Valid,
            type: AuthorizationTypes.Permanent,
            scopes: request.GetScopes()).ToListAsync();
    }

    #endregion Private Methods
}