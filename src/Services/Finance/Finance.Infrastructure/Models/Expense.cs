namespace PAD.Finance.Infrastructure.Models;

public class Expense : BaseEntity<string>
{
    #region Foreign keys
    
    public string CategoryId { get; set; }

    #endregion Foreign keys

    public string AuthorId { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }

    
    #region Virtual fields

    public virtual ExpenseCategory Category { get; set; }

    #endregion Virtual fields
}