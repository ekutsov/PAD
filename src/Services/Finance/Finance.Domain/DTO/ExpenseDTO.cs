namespace PAD.Finance.Domain.DTO;

public class ExpenseDTO
{
    public string CategoryId { get; set; }

    public string Description { get; set; }

    protected DateTime CreatedDate { get; set; }

    public double Amount { get; set; }

    public bool IsExcluded { get; set; }
}