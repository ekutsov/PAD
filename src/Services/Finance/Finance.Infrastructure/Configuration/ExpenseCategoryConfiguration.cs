namespace PAD.Finance.Infrastructure.Configuration;

public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.HasData(new ExpenseCategory[] {
            new ExpenseCategory()
            {
                Id = Guid.NewGuid(),
                Name = "Clothes and Shoes"
            },
            new ExpenseCategory()
            {
                Id = Guid.NewGuid(),
                Name = "Medecine and pharmacies"
            },
            new ExpenseCategory()
            {
                Id = Guid.NewGuid(),
                Name = "Pets"
            },
            new ExpenseCategory()
            {
                Id = Guid.NewGuid(),
                Name = "Foodstuff"
            },
            new ExpenseCategory()
            {
                Id = Guid.NewGuid(),
                Name = "Utility bills"
            }
        });
    }
}