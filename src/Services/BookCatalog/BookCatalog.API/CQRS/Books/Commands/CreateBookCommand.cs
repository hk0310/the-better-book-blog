using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.CQRS.Books.Commands;

public class CreateBookCommand : ICommand<Book>
{
    public string Title { get; set; }
    public string Synopsis { get; set; }

    public string Isbn { get; set; }
    public int PageCount { get; set; }
    public DateOnly PublishDate { get; set; }
    public int AuthorId { get; set; }
    public List<int> GenreIds { get; set; }
}

public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Book>
{
    private readonly IBookCatalogContext _context;

    public CreateBookCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var genres = await _context.Genres.Where(x => request.GenreIds.Contains(x.Id)).ToListAsync(cancellationToken);
        var isbn = new Isbn(request.Isbn);

        var book = new Book()
        {
            Title = request.Title,
            Synopsis = request.Synopsis,
            Isbn = isbn,
            PageCount = request.PageCount,
            PublishDate = request.PublishDate,
            AuthorId = request.AuthorId,
            Genres = genres
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync(cancellationToken);

        return book;
    }
}
