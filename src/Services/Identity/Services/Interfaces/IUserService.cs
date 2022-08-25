namespace PAD.Identity.Services;

public interface IUserService
{
    Task<ApplicationUser> FindByIdAsync(string userId);

    Task<string> GetUserIdAsync(ApplicationUser user);
    
    Task<ImmutableArray<string>> GetRolesAsync(ApplicationUser user);
    
    Task<ApplicationUser> GetByClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal);
}