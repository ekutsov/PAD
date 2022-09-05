namespace PAD.Finance.Infrastructure.Models;

public abstract class BaseEntity<T>
{
    protected T Id { get; set; }

    protected DateTime CreatedDate { get; set; }

    protected DateTime UpdatedDate { get; set; }
}