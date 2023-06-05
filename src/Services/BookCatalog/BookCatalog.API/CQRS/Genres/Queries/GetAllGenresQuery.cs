using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Genres.Queries;

public class GetAllGenresQuery : IRequest<IEnumerable<Genre>> { }

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<Genre>>
{
    private readonly IBookCatalogContext _context;

    public GetAllGenresQueryHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        var genres = await _context.Genres.ToListAsync(cancellationToken);

        return genres.AsReadOnly();
    }
}