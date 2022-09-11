namespace PAD.Finance.Domain.ViewModels;

public class ExpenseViewModel
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }

    public ExpenseCategoryViewModel Category { get; set; }
}