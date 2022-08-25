namespace PFA.Identity.Services;

public interface IAuthService
{
    Task<AuthorizeDTO> AuthorizeAsync();

    Task<AuthorizeDTO> AcceptAsync();

    Task<AuthorizeDTO> SignOutAsync();

    Task<AuthorizeDTO> ExchanegAsync();
}