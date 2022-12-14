namespace PAD.Identity.API.Workers;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync(cancellationToken);

        await RegisterApplicationsAsync(scope.ServiceProvider, cancellationToken);
        await RegisterScopesAsync(scope.ServiceProvider);

        static async Task RegisterApplicationsAsync(IServiceProvider provider, CancellationToken cancellationToken)
        {
            IOpenIddictApplicationManager manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("web_client", cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "web_client",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "Blazor client application",
                    Type = ClientTypes.Public,
                    PostLogoutRedirectUris =
                    {
                        new Uri("https://localhost:3000/authentication/logout-callback")
                    },
                    RedirectUris =
                    {
                        new Uri("https://localhost:3000/authentication/login-callback"),
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles,
                        Permissions.Prefixes.Scope + "api"
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                }, cancellationToken);
            }

            if (await manager.FindByClientIdAsync("finance_service") is null)
            {
                OpenIddictApplicationDescriptor descriptor = new()
                {
                    ClientId = "finance_service",
                    ClientSecret = "846B62D0-DEF9-4215-A99D-86E6B8DAB342",
                    Permissions = { Permissions.Endpoints.Introspection }
                };

                await manager.CreateAsync(descriptor, cancellationToken);
            }
        }

        static async Task RegisterScopesAsync(IServiceProvider provider)
        {
            IOpenIddictScopeManager manager = provider.GetRequiredService<IOpenIddictScopeManager>();

            if (await manager.FindByNameAsync("api") is null)
            {
                await manager.CreateAsync(new OpenIddictScopeDescriptor
                {
                    DisplayName = "API Access",
                    Name = "api",
                    Resources =
                    {
                        "finance_service",
                    }
                });
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}