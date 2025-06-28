using Ardalis.GuardClauses;

namespace RiverBooks.Users;

internal record UserAddress
{
    private UserAddress() { }

    public UserAddress(string userId, Address address)
    {
        UserId = Guard.Against.NullOrEmpty(userId);
        Address = Guard.Against.Null(address);
    }

    public Guid Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public Address Address { get; private set; } = default!;
}
