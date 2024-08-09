using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Authors.Queries;

public class GetAllAuthorsQuery : IQuery<IEnumerable<Author>> { }

public class GetAllAuthorsQueryHandler : IQueryHandler<GetAllAuthorsQuery, IEnumerable<Author>>
{
    private readonly IBookCatalogContext _context;

    public GetAllAuthorsQueryHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _context.Authors.ToListAsync(cancellationToken);

        return authors.AsReadOnly();
    }
}