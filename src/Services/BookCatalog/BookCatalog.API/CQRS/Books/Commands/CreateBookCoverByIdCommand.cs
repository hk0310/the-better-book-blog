﻿using BookCatalog.API.Infrastructure;
using MediatR;
using System.Buffers.Text;

namespace BookCatalog.API.CQRS.Books.Commands;

public class CreateBookCoverByIdCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Encoding { get; set; }
}

public class CreateBookCoverByIdCommandHandler : IRequestHandler<CreateBookCoverByIdCommand, bool>
{
    private readonly IBookCatalogContext _context;

    public CreateBookCoverByIdCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateBookCoverByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(request.Id);

        if (book == null)
        {
            return false;
        }

        var imageBytes = Convert.FromBase64String(request.Encoding);
        var imagePath = Constants.BookCoverPath + Guid.NewGuid().ToString() + ".jpeg";

        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length, cancellationToken);
            await fileStream.FlushAsync();
        }

        if (book.CoverImagePath != null && File.Exists(book.CoverImagePath))
        {
            File.Delete(book.CoverImagePath);
        }

        book.CoverImagePath = imagePath;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
