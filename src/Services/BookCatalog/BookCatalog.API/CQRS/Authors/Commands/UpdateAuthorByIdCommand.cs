using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;

namespace BookCatalog.API.CQRS.Authors.Commands;

public class UpdateAuthorByIdCommand : ICommand<bool>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Introduction { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Website { get; set; }
    public string Twitter { get; set; }
}

public class UpdateAuthorByIdCommandHandler : ICommandHandler<UpdateAuthorByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public UpdateAuthorByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FindAsync(request.Id, cancellationToken);

        if (author == null)
        {
            return false;
        }

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.Introduction = request.Introduction;
        author.DateOfBirth = request.DateOfBirth;
        author.Website = new Uri(request.Website, UriKind.Absolute);
        author.Twitter = request.Twitter;
        await _context.SaveChangesAsync(cancellationToken);

        return true;

    }
}
