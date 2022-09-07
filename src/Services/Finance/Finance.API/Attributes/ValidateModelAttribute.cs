using Microsoft.AspNetCore.Mvc.Filters;

namespace APS.Web.Filters
{
    public class ValidateModelAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid == false)
            {
                
                List<string> errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage).ToList();
                throw new InvalidOperationException("Failed request body validation");
            }
            await next();
        }
    }
}
