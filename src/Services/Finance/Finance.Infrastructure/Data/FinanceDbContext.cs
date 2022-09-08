namespace PAD.Finance.Infrastructure.Data;

public class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

    public DbSet<Expense> Expsenses { get; set; }

    public DbSet<ExpenseCategory> ExpsenseCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Override the "SaveChangesAsync" basic method to automatically add created and updated dates
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Is accept all changes on success</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns></returns>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
                                CancellationToken cancellationToken = default)
    {
        foreach (var entity in ChangeTracker.Entries())
        {
            if (entity.State == EntityState.Added && entity.Metadata.FindProperty("CreatedDate") != null)
            {
                entity.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}

// public class FinanceDbContextFactory : IDesignTimeDbContextFactory<FinanceDbContext>
// {
//     public FinanceDbContext CreateDbContext(string[] args)
//     {
//         DbContextOptionsBuilder<FinanceDbContext> optionsBuilder = new();
//         optionsBuilder.UseNpgsql("Host=localhost;Port=6432;User ID=finance_admin;Database=finance;Password=supersecretpassword;");

//         return new FinanceDbContext(optionsBuilder.Options);
//     }
// }