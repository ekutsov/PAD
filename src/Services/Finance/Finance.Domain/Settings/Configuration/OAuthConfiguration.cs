namespace PAD.Finance.Domain.Settings;

public class OAuthConfiguration : IConfigureOptions<OAuthOptions>
{
    public void Configure(OAuthOptions options)
    {
        options.Issuer = Environment.GetEnvironmentVariable(OAuthVariables.Issuer);
        options.Audience = Environment.GetEnvironmentVariable(OAuthVariables.Audience);
        options.ClientId = Environment.GetEnvironmentVariable(OAuthVariables.ClientId);
        options.ClientSecret = Environment.GetEnvironmentVariable(OAuthVariables.ClientSecret);
    }
}