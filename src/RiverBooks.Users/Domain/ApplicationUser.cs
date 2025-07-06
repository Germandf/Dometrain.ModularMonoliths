using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using RiverBooks.SharedKernel;

namespace RiverBooks.Users.Domain;

internal class ApplicationUser : IdentityUser, IHaveDomainEvents
{
    public string? FullName { get; set; }

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    private readonly List<UserAddress> _addresses = new();
    public IReadOnlyCollection<UserAddress> Addresses => _addresses.AsReadOnly();

    private List<DomainEventBase> _domainEvents = new();
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

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

    internal void ClearCart()
    {
        _cartItems.Clear();
    }

    internal UserAddress AddAddress(Address address)
    {
        Guard.Against.Null(address);
        var existingAddress = _addresses.SingleOrDefault(a => a.Address == address);
        if (existingAddress is not null)
            return existingAddress;
        var newAddress = new UserAddress(Id, address);
        _addresses.Add(newAddress);
        var domainEvent = new AddressAddedEvent(newAddress);
        RegisterDomainEvent(domainEvent);
        return newAddress;
    }

    private void RegisterDomainEvent(AddressAddedEvent domainEvent)
    {
        Guard.Against.Null(domainEvent);
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
