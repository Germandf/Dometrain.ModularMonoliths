using RiverBooks.SharedKernel;

namespace RiverBooks.Users.Domain;

internal sealed class AddressAddedEvent : DomainEventBase
{
    public AddressAddedEvent(UserAddress newAddress)
    {
        NewAddress = newAddress;
    }

    public UserAddress NewAddress { get; }
}
