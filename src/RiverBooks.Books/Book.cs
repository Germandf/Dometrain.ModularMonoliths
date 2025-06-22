using Ardalis.GuardClauses;

namespace RiverBooks.Books;

internal class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }

    public Book(Guid id, string title, string author, decimal price)
    {
        Id = Guard.Against.Default(id);
        Title = Guard.Against.NullOrEmpty(title);
        Author = Guard.Against.NullOrEmpty(author);
        Price = Guard.Against.Negative(price);
    }

    internal void UpdatePrice(decimal newPrice)
    {
        Price = Guard.Against.Negative(newPrice);
    }
}
