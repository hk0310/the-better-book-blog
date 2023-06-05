﻿using MediatR;
using BookCatalog.API.Models;
using BookCatalog.API.Infrastructure;

namespace BookCatalog.API.CQRS.Books.Queries;

public class GetBookByIdQuery : IRequest<Book>
{
    public int Id { get; set; }
}

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
{
    private readonly IBookCatalogContext _context;

    public GetBookByIdQueryHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(request.Id, cancellationToken);

        return book;
    }
}
