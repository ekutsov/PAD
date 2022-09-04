using Microsoft.EntityFrameworkCore;
using PAD.Finance.Models;

namespace PAD.Finance.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<ExpenseCategory> ExpsenseCategories { get; set; }
}