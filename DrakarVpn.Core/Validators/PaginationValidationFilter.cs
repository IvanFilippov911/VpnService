using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DrakarVpn.Domain.Models.Pagination;

namespace DrakarVpn.Core.Validators;

public class PaginationValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg is IPaginatable paged)
            {
                if (paged.Offset < 0 || paged.Limit <= 0 || paged.Limit > 500)
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        success = false,
                        error = "Invalid pagination parameters. Offset must be >= 0, Limit must be in range 1–500."
                    });
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
