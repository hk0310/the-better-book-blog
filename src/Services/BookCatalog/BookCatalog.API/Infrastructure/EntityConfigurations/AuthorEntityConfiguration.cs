using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCatalog.API.Infrastructure.EntityConfigurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable(nameof(Author));

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .UseHiLo("author_hilo")
            .IsRequired();

        builder.Property(a => a.FirstName)
            .IsRequired();
    }
}
