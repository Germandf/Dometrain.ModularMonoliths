using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiverBooks.Books.Domain;

namespace RiverBooks.Books.Infrastructure.Data;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    internal static readonly Guid Book1Guid = Guid.Parse("d1b2c3f4-5e6f-4a8b-9c0d-e1f2a3b4c5d6");
    internal static readonly Guid Book2Guid = Guid.Parse("a1b2c3d4-e5f6-4a8b-9c0d-e1f2a3b4c5d6");
    internal static readonly Guid Book3Guid = Guid.Parse("f1e2d3c4-b5a6-4b8c-9d0e-f1a2b3c4d5e6");

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(p => p.Title)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(p => p.Author)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.HasData(GetSampleBookData());
    }

    private IEnumerable<Book> GetSampleBookData()
    {
        var tolkien = "J.R.R. Tolkien";
        yield return new Book(Book1Guid, "The Fellowship of the Ring", tolkien, 10.99m);
        yield return new Book(Book2Guid, "The Two Towers", tolkien, 11.99m);
        yield return new Book(Book3Guid, "The Return of the King", tolkien, 12.99m);
    }
}
