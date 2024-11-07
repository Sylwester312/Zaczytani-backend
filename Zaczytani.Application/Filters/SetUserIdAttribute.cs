using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Zaczytani.Application.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SetUserIdAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is IUserIdAssignable userRequest)
                {
                    userRequest.SetUserId(userId);
                }
            }
        }
    }
}
