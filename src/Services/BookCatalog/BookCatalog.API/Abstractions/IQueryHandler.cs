using MediatR;

namespace BookCatalog.API.Abstractions
{
    public interface IQueryHandler<in TQuery, TRespond> : IRequestHandler<TQuery, TRespond> where TQuery : IQuery<TRespond>
    {
    }
}
