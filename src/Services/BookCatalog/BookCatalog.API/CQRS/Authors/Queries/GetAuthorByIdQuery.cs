using MediatR;
using BookCatalog.API.Models;
using BookCatalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Authors.Queries;

public class GetAuthorByIdQuery : IRequest<Author>
{
    public int Id { get; set; }
}

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
{
    private readonly IBookCatalogContext _context;

    public GetAuthorByIdQueryHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FindAsync(request.Id, cancellationToken);

        return author;
    }
}
