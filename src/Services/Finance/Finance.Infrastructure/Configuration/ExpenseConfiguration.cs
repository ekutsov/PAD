using System.ComponentModel.DataAnnotations.Schema;

namespace PAD.Finance.Infrastructure.Configuration;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasOne(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId);
    }
}