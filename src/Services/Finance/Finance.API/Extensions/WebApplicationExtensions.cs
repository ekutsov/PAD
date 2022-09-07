namespace PAD.Finance.API.Extensions;

public static class WebApplicationExtensions
{
    public static void UseApplicationCors(this WebApplication app)
    {
        ApiGatewayOptions options = app.Services
            .GetRequiredService<IOptions<ApiGatewayOptions>>()
            .Value;

        app.UseCors(builder => builder
            .WithOrigins(options.Origin)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
    }
}