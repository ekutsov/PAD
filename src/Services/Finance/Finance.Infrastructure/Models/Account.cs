namespace PAD.Finance.Infrastructure.Models;

public class Account : BaseEntity
{
    public string Name { get; set; }

    /// <summary>
    /// Identifier of the user from the identity service that has an account
    /// </summary>
    /// <value></value>
    public Guid AuthorId { get; set; }
}