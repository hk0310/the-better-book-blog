using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;

namespace BookCatalog.API.CQRS.Books.Commands;

public class DeleteBookByIdCommand : ICommand<bool>
{
    public int Id { get; set; }
}

public class DeleteBookByIdCommandHandler : ICommandHandler<DeleteBookByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public DeleteBookByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(request.Id, cancellationToken);

        if (book == null)
        {
            return false;
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
