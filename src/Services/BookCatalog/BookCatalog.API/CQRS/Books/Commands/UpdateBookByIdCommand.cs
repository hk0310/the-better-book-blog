using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Books.Commands;

public class UpdateBookByIdCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishDate { get; set; }
    public int AuthorId { get; set; }
    public List<int> GenreIds { get; set; }
}

public class UpdateBookByIdCommandHandler : IRequestHandler<UpdateBookByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public UpdateBookByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(request.Id, cancellationToken);

        if(book == null)
        {
            return false;
        }

        var genres = await _context.Genres.Where(x => request.GenreIds.Contains(x.Id)).ToListAsync(cancellationToken);

        book.Genres = genres;
        book.Title = request.Title;
        book.Synopsis = request.Synopsis;
        book.PageCount = request.PageCount;
        book.PublishDate = request.PublishDate;
        book.AuthorId = request.AuthorId;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
