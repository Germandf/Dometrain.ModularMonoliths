using MediatR;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;
using RiverBooks.Users.Contracts;

namespace RiverBooks.OrderProcessing.Integrations;

internal class AddressAddedIntegrationEventHandler(IAddressCache addressCache) : INotificationHandler<AddressAddedIntegrationEvent>
{
    public async Task Handle(AddressAddedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var address = new Address(
            notification.NewAddress.Street1,
            notification.NewAddress.Street2,
            notification.NewAddress.City,
            notification.NewAddress.State,
            notification.NewAddress.PostalCode,
            notification.NewAddress.Country);
        await addressCache.StoreAsync(notification.NewAddress.Id, address);
    }
}
