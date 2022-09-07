namespace PAD.Finance.Domain.Settings;

public class OAuthOptions
{
    public string Issuer { get; set; }

    public string Audience { get; set; }
    
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }
}