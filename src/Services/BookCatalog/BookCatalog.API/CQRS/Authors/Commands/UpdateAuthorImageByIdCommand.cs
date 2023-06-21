using BookCatalog.API.CQRS.Books.Commands;
using BookCatalog.API.Infrastructure;
using MediatR;

namespace BookCatalog.API.CQRS.Authors.Commands;

public class UpdateAuthorImageByIdCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Encoding { get; set; }
}

public class UpdateAuthorImageByIdCommandHandler : IRequestHandler<UpdateAuthorImageByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public UpdateAuthorImageByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateAuthorImageByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await _context.Authors.FindAsync(request.Id);

        if (author == null)
        {
            return false;
        }

        var imageBytes = Convert.FromBase64String(request.Encoding);
        var imagePath = Constants.AuthorImagePath + Guid.NewGuid().ToString() + ".jpeg";

        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length, cancellationToken);
            await fileStream.FlushAsync();
        }

        if (author.ImagePath != null && File.Exists(author.ImagePath))
        {
            File.Delete(author.ImagePath);
        }

        author.ImagePath = imagePath;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
