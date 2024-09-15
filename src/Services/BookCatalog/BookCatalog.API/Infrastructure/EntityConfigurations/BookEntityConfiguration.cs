using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API.Infrastructure.EntityConfigurations
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));

            builder.HasKey(b => b.Id);
            builder.HasAlternateKey(b => b.Isbn);

            builder.Property(b => b.Id)
                .UseHiLo("book_hilo")
                .IsRequired();

            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.PublishDate)
                .IsRequired();

            builder.Property(b => b.Isbn)
                .HasConversion(v => v.ToString(),
                               v => new Isbn(v));
        }
    }
}
