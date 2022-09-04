using System.ComponentModel.DataAnnotations;

namespace PAD.Finance.Models;

public class ExpenseCategory
{
    [Key]
    public Guid Id { get; set; }
}