namespace PAD.Client.Models;

public class Expense
{
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }

    public ExpenseCategory Category { get; set; }
}