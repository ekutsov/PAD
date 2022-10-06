using System.ComponentModel.DataAnnotations;

namespace PAD.Client.Models;

public class ExpenseDTO
{
    public ExpenseDTO() { }
    public ExpenseDTO(Expense expense, int timezoneOffset)
    {
        CategoryId = expense.CategoryId;
        CreatedDate = expense.CreatedDate.AddMinutes(timezoneOffset);
        Description = expense.Description;
        Amount = expense.Amount;
    }

    [Required(ErrorMessage = "The Category is required")]
    public Guid? CategoryId { get; set; }

    [Required(ErrorMessage = "The Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The CreatedDate is required")]
    public DateTime? CreatedDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "The Amount is required")]
    public double? Amount { get; set; }
}