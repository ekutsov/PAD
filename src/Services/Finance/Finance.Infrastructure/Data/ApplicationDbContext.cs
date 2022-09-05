using Microsoft.EntityFrameworkCore;
using PAD.Finance.Infrastructure.Models;

namespace PAD.Finance.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Expense> Expsenses { get; set; }

    public DbSet<ExpenseCategory> ExpsenseCategories { get; set; }
}