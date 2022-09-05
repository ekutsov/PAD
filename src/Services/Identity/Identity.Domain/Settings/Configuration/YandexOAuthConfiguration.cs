using Microsoft.Extensions.Options;
using PAD.Identity.Domain.Constants;

namespace PAD.Identity.Domain.Settings;

public class YandexOAuthConfiguration : IConfigureOptions<YandexOAuthOptions>
{
    public void Configure(YandexOAuthOptions options)
    {
        options.ClientId = Environment.GetEnvironmentVariable(YandexOAuthVariables.ClientId);
        options.ClientSecret = Environment.GetEnvironmentVariable(YandexOAuthVariables.ClientSecret);
        options.CallbackPath = Environment.GetEnvironmentVariable(YandexOAuthVariables.CallbackPath);
    }
}