using Ardalis.GuardClauses;

namespace RiverBooks.Users;

internal class CartItem
{
    public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
    {
        BookId = Guard.Against.Default(bookId);
        Description = Guard.Against.NullOrEmpty(description);
        Quantity = Guard.Against.Negative(quantity);
        UnitPrice = Guard.Against.Negative(unitPrice);
    }

    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    internal void UpdateDescription(string description) =>
        Description = Guard.Against.NullOrEmpty(description);

    internal void UpdateQuantity(int quantity) =>
        Quantity = Guard.Against.Negative(quantity);

    internal void UpdateUnitPrice(decimal unitPrice) =>
        UnitPrice = Guard.Against.Negative(unitPrice);
}
