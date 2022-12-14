namespace PAD.Finance.Infrastructure.Models;

public class Expense : BaseEntity
{
    #region Foreign keys
    /// <summary>
    /// Expense category id
    /// </summary>
    /// <value></value>
    public Guid CategoryId { get; set; }

    #endregion Foreign keys

    /// <summary>
    /// Identifier of the user from the identity service that created the expense
    /// </summary>
    /// <value></value>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Expense description
    /// </summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Expense record created date
    /// </summary>
    /// <value></value>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Amount of expense
    /// </summary>
    /// <value></value>
    public double Amount { get; set; }

    /// <summary>
    /// Is excluded from the calculation statistics
    /// </summary>
    /// <value></value>
    public bool IsExcluded { get; set; }


    #region Virtual fields

    /// <summary>
    /// Expense category
    /// </summary>
    /// <value></value>
    public virtual ExpenseCategory Category { get; set; }

    #endregion Virtual fields
}