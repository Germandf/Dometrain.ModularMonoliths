using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace RiverBooks.Users;

internal class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public void AddItemToCart(CartItem item)
    {
        Guard.Against.Null(item);
        var existingItem = _cartItems.FirstOrDefault(ci => ci.BookId == item.BookId);
        if (existingItem is not null)
            existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);
        else
            _cartItems.Add(item);
    }
}
