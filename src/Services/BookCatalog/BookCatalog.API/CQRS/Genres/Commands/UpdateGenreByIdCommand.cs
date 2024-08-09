using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;

namespace BookCatalog.API.CQRS.Genres.Commands;

public class UpdateGenreByIdCommand : ICommand<bool>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class UpdateGenreByIdCommandHandler : ICommandHandler<UpdateGenreByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public UpdateGenreByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateGenreByIdCommand request, CancellationToken cancellationToken)
    {
        var genre = await _context.Genres.FindAsync(request.Id, cancellationToken);

        if (genre == null)
        {
            return false;
        }

        genre.Name = request.Name;
        genre.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
