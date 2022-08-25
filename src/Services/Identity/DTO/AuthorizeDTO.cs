namespace PFA.Identity.DTO;

public class AuthorizeDTO
{
    public AuthorizeDTO(ClaimsIdentity identity)
    {
        Identity = new ClaimsPrincipal(identity);
    }

    public AuthorizeDTO(ClaimsPrincipal principal)
    {
        Identity = principal;
    }

    public AuthorizeDTO(string redirectUri)
    {
        RedirectUri = redirectUri;
    }

    public AuthorizeDTO(string error, string errorDescription)
    {
        Error = error;
        ErrorDescription = errorDescription;
    }

    public AuthorizeDTO() { }

    public string RedirectUri { get; set; }

    public string Error { get; set; }

    public string ErrorDescription { get; set; }

    public ClaimsPrincipal Identity { get; set; }

    public AuthorizeViewModel ViewModel { get; set; }
}