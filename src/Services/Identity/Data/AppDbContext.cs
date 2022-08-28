using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PAD.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
}