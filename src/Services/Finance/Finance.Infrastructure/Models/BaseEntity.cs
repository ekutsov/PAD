namespace PAD.Finance.Infrastructure.Models;

public abstract class BaseEntity<T>
{
    public T Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}