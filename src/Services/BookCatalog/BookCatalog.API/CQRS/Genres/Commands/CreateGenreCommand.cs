﻿using BookCatalog.API.Abstractions;
using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;

namespace BookCatalog.API.CQRS.Genres.Commands;

public class CreateGenreCommand : ICommand<Genre>
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreateGenreCommandHandler : ICommandHandler<CreateGenreCommand, Genre>
{
    private readonly IBookCatalogContext _context;

    public CreateGenreCommandHandler(IBookCatalogContext context)
    {
        _context = context;
    }

    public async Task<Genre> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre()
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync(cancellationToken);

        return genre;
    }
}
