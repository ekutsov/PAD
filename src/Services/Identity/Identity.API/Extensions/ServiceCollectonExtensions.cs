namespace PAD.Identity.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<YandexOAuthConfiguration>();
    }

    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            options.UseOpenIddict();
        });
    }

    public static void AddIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationDbContext>()
             .AddDefaultTokenProviders()
             .AddDefaultUI();
    }

    public static void AddQuartz(this WebApplicationBuilder builder)
    {
        builder.Services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });

        builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }

    public static void AddExternalAuthentication(this WebApplicationBuilder builder)
    {
        YandexOAuthOptions yandexOptions = builder.Services.BuildServiceProvider()
            .GetRequiredService<IOptions<YandexOAuthOptions>>().Value;

        builder.Services.AddAuthentication()
            .AddYandex(options =>
            {
                options.ClientId = yandexOptions.ClientId;
                options.ClientSecret = yandexOptions.ClientSecret;
                options.CallbackPath = yandexOptions.CallbackPath;
            });
    }

    public static void AddOpenIdConnectAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
                options.UseQuartz();
            })
            .AddServer(options =>
            {
                options.SetAuthorizationEndpointUris("/connect/authorize")
                    .SetLogoutEndpointUris("/connect/logout")
                    .SetTokenEndpointUris("/connect/token")
                    .SetUserinfoEndpointUris("/connect/userinfo")
                    .SetIntrospectionEndpointUris("/connect/introspect");

                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);

                options.AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableStatusCodePagesIntegration()
                    .EnableTokenEndpointPassthrough();

                options.UseReferenceAccessTokens()
                    .UseReferenceRefreshTokens();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IApplicationService, ApplicationService>()
                        .AddScoped<IAuthService, AuthService>()
                        .AddScoped<IScopeService, ScopeService>()
                        .AddScoped<IUserService, UserService>();
    }
}