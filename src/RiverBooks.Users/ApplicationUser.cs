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
            UpdateExistingItem(existingItem, item);
        else
            _cartItems.Add(item);
    }

    private void UpdateExistingItem(CartItem existingItem, CartItem item)
    {
        existingItem.UpdateDescription(item.Description);
        existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);
        existingItem.UpdateUnitPrice(item.UnitPrice);
    }
}
