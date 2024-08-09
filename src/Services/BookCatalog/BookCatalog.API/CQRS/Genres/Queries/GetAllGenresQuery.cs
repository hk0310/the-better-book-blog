using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Genres.Queries;

public class GetAllGenresQuery : IQuery<IEnumerable<Genre>> { }

public class GetAllGenresQueryHandler : IQueryHandler<GetAllGenresQuery, IEnumerable<Genre>>
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