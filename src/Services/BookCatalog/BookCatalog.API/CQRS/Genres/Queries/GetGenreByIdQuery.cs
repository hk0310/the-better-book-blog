using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Genres.Queries;

public class GetGenreByIdQuery : IRequest<Genre>
{
    public int Id { get; set; }
}

public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Genre>
{
    private readonly IBookCatalogContext _context;

    public GetGenreByIdQueryHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<Genre> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Genres.Include(g => g.Books).FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        return book;
    }
}
