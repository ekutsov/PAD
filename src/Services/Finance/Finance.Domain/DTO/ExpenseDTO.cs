namespace PAD.Finance.Domain.DTO;

public class ExpenseDTO
{
    public Guid CategoryId { get; set; }

    public string Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public double Amount { get; set; }
}