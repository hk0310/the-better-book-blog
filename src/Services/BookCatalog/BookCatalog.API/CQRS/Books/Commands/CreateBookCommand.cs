using MediatR;
using BookCatalog.API.Models;
using BookCatalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Books.Commands;

public class CreateBookCommand : IRequest<Book>
{
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public int PageCount { get; set; }
    public DateTime DatePublished { get; set; }
    public int AuthorId { get; set; }
    public List<int> GenreIds { get; set; }   
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
{
    private readonly IBookCatalogContext _context;

    public CreateBookCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var genres = await _context.Genres.Where(x => request.GenreIds.Contains(x.Id)).ToListAsync(cancellationToken);

        var book = new Book()
        {
            Title = request.Title,
            Synopsis = request.Synopsis,
            PageCount = request.PageCount,
            DatePublished = request.DatePublished,
            AuthorId = request.AuthorId,
            Genres = genres
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);

        return book;
    }
}
