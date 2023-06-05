using MediatR;
using BookCatalog.API.Models;
using BookCatalog.API.Infrastructure;

namespace BookCatalog.API.CQRS.Authors.Commands;

public class DeleteAuthorByIdCommand : IRequest<bool>
{
    public int Id { get; set; }
}

public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public DeleteAuthorByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FindAsync(request.Id, cancellationToken);

        if (author == null) 
        {
            return false;
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
