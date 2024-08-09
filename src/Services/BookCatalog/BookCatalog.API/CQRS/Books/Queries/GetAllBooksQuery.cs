using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Books.Queries;

public class GetAllBooksQuery : IQuery<IEnumerable<Book>> { }

public class GetAllBookQueryHandler : IQueryHandler<GetAllBooksQuery, IEnumerable<Book>>
{
    private readonly IBookCatalogContext _context;

    public GetAllBookQueryHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _context.Books.ToListAsync(cancellationToken);

        return books.AsReadOnly();
    }
}
