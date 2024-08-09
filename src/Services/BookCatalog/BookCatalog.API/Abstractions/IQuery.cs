using MediatR;

namespace BookCatalog.API.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
