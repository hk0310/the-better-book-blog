using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Authors.Queries;

public class GetAuthorByIdQuery : IQuery<Author>
{
    public int Id { get; set; }
}

public class GetAuthorByIdQueryHandler : IQueryHandler<GetAuthorByIdQuery, Author>
{
    private readonly IBookCatalogContext _context;

    public GetAuthorByIdQueryHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        return author;
    }
}
