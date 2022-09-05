using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PAD.Identity.Domain.ViewModels;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}