using System.ComponentModel.DataAnnotations;

namespace PAD.Client.Models;

public class ExpenseDTO
{
    [Required(ErrorMessage = "The Category is required")]
    public string CategoryId { get; set; }

    [Required(ErrorMessage = "The Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The CreatedDate is required")]
    public DateTime CreatedDate { get; set; }

    [Required(ErrorMessage = "The Amount is required")]
    public double? Amount { get; set; }
}