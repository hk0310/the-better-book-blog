using BookCatalog.API.Infrastructure;
using MediatR;

namespace BookCatalog.API.CQRS.Genres.Commands;

public class DeleteGenreByIdCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteGenreByIdCommandHandler : IRequestHandler<DeleteGenreByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public DeleteGenreByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteGenreByIdCommand request, CancellationToken cancellationToken)
    {
        var genre = await _context.Genres.FindAsync(request.Id, cancellationToken);

        if(genre == null)
        {
            return false;
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
