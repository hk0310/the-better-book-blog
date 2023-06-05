using BookCatalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCatalog.API.Infrastructure.EntityConfigurations
{
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable(nameof(Genre));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseHiLo("genre_hilo")
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
