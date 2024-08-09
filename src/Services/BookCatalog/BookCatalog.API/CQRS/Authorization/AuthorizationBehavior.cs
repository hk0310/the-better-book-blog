using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using MediatR;

namespace BookCatalog.API.CQRS.Authentication;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, ICommand<TResponse>
{
    private readonly IUserAccessor _userAccessor;

    public AuthorizationBehavior(IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var role = _userAccessor.User.Claims.Where(c => c.Type == "role").FirstOrDefault();

        if (role == null || role.Value != "Administrator")
        {
            throw new UnauthorizedAccessException("Unauthorized access.");
        }

        return await next();
    }
}
