namespace PFA.Server.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ApplicationUser> FindByIdAsync(string userId)
    {
        ApplicationUser user = await _userManager.FindByIdAsync(userId);

        return user;
    }

    public async Task<string> GetUserIdAsync(ApplicationUser user)
    {
        string userId = await _userManager.GetUserIdAsync(user);

        return userId;
    }

    public async Task<ImmutableArray<string>> GetRolesAsync(ApplicationUser user)
    {
        IList<string> roles = await _userManager.GetRolesAsync(user);

        return roles.ToImmutableArray();
    }

    public async Task<ApplicationUser> GetByClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal)
    {
        ApplicationUser user = await _userManager.GetUserAsync(claimsPrincipal);

        if (user == null)
        {
            throw new InvalidOperationException("The user details cannot be retrieved.");
        }

        return user;
    }
}