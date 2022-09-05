using PAD.Identity.Domain.DTO;

namespace PAD.Identity.Core.Services;

public interface IAuthService
{
    Task<AuthorizeDTO> AuthorizeAsync();

    Task<AuthorizeDTO> AcceptAsync();

    Task<AuthorizeDTO> SignOutAsync();

    Task<AuthorizeDTO> ExchanegAsync();
}