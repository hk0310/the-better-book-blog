using MediatR;
using BookCatalog.API.Models;
using BookCatalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Books.Queries;

public class GetAllBooksQuery : IRequest<IEnumerable<Book>> { }

public class GetAllBookQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
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
