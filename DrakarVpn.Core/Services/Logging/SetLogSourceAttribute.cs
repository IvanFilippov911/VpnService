using DrakarVpn.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DrakarVpn.Core.Services.Logging;

public class SetLogSourceAttribute : ActionFilterAttribute
{
    private readonly SystemLogSource _source;

    public SetLogSourceAttribute(SystemLogSource source)
    {
        _source = source;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Items["SystemLogSource"] = _source;
        base.OnActionExecuting(context);
    }
}