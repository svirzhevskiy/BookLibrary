using BookLibrary.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibrary.Database.EntityConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Year).IsRequired().HasColumnType("SMALLINT");
            builder.Property(x => x.ISBN).HasMaxLength(10);
            builder.Property(x => x.Blurb).HasMaxLength(1000);

            builder
                .HasGeneratedTsVectorColumn(
                    x => x.SearchVector,
                    "english",
                    x => new {x.Title, x.Blurb})
                .HasIndex(x => x.SearchVector)
                .HasMethod("GIN");

        }
    }
}