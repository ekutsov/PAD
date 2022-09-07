namespace PAD.Finance.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureOptions<OAuthConfiguration>();
        builder.Services.ConfigureOptions<ApiGatewayConfiguration>();
    }

    public static void AddFinanceDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<FinanceDbContext>(options => 
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IExpenseService, ExpenseService>();
    }

    public static void AddOpenIdAuthentication(this WebApplicationBuilder builder)
    {
        OAuthOptions opts = builder.Services.BuildServiceProvider()
            .GetRequiredService<IOptions<OAuthOptions>>().Value;

        builder.Services.AddOpenIddict().AddValidation(options =>
        {
            options.SetIssuer(opts.Issuer);
            options.AddAudiences(opts.Audience);

            options.UseIntrospection()
                .SetClientId(opts.ClientId)
                .SetClientSecret(opts.ClientSecret);

            options.UseSystemNetHttp();

            options.UseAspNetCore();
        });

        builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        builder.Services.AddAuthorization();
    }

    public static void AddSwagger(this WebApplicationBuilder builder)
    {

        builder.Services.AddSwaggerGen(c =>
        {
            OpenApiSecurityScheme securityScheme = new()
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    securityScheme,
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void AddControllersAndEndpoints(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
    }
}