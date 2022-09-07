namespace PAD.Finance.Domain.Settings;

public class ApiGatewayConfiguration : IConfigureOptions<ApiGatewayOptions>
{
    public void Configure(ApiGatewayOptions options)
    {
        options.Origin = Environment.GetEnvironmentVariable(ApiGatewayVariables.Origin);
    }
}