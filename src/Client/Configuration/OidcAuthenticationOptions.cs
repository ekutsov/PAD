namespace PAD.Client.Configuration;

public class OidcAuthenticationOptions
{
    public string Authority { get; set; }

    public string ClientId { get; set; }

    public string ResponseType { get; set; }

    public string ResponseMode { get; set; }

    public string DefaultScope { get; set; }

    public string RoleClaim { get; set; }
}