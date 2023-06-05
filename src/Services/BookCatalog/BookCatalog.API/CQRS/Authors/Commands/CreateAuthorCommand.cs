using MediatR;
using BookCatalog.API.Models;
using BookCatalog.API.Infrastructure;

namespace BookCatalog.API.CQRS.Authors.Commands;

public class CreateAuthorCommand : IRequest<Author>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Introduction { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Website { get; set; }
    public string Twitter { get; set; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
{
    private readonly IBookCatalogContext _context;

    public CreateAuthorCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }
    
    public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Introduction = request.Introduction,
            DateOfBirth = request.DateOfBirth,
            Website = new Uri(request.Website, UriKind.Absolute),
            Twitter = request.Twitter
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync(cancellationToken);

        return author;
    }
}
