using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PFA.Server.ViewModels;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}