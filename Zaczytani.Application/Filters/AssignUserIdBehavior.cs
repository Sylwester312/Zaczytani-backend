using MediatR;
using Microsoft.AspNetCore.Http;
using Zaczytani.Application.Filters;

internal class AssignUserIdBehavior<TRequest, TResponse>(IHttpContextAccessor httpContextAccessor) : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IUserIdAssignable userRequest)
        {
            if (_httpContextAccessor.HttpContext?.Items["UserId"] is Guid userId)
            {
                userRequest.SetUserId(userId);
            }
        }

        return await next();
    }
}
