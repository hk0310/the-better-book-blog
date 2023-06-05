using MediatR;
using BookCatalog.API.Models;
using BookCatalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookCatalog.API.CQRS.Authors.Queries;

public class GetAllAuthorsQuery : IRequest<IEnumerable<Author>> { }

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
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