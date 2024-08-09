using MediatR;

namespace BookCatalog.API.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
