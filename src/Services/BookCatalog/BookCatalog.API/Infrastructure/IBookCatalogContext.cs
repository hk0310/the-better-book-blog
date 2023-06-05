using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Infrastructure
{
    public interface IBookCatalogContext
    {
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}