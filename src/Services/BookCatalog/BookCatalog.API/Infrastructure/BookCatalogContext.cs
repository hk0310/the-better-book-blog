using BookCatalog.API.Infrastructure.EntityConfigurations;
using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Infrastructure;

public class BookCatalogContext : DbContext, IBookCatalogContext
{
    public BookCatalogContext(DbContextOptions<BookCatalogContext> options) : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AuthorEntityConfiguration());
        builder.ApplyConfiguration(new BookEntityConfiguration());
        builder.ApplyConfiguration(new GenreEntityConfiguration());

        builder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .IsRequired();

        builder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
