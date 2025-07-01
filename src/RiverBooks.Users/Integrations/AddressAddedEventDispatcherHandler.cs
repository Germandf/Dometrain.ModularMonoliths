using MediatR;
using RiverBooks.Users.Contracts;

namespace RiverBooks.Users.Integrations;

internal class AddressAddedEventDispatcherHandler(IMediator mediator) : INotificationHandler<AddressAddedEvent>
{
    public async Task Handle(AddressAddedEvent notification, CancellationToken ct)
    {
        var userId = Guid.Parse(notification.NewAddress.UserId);

        var addressDetails = new AddressDetails(
            userId,
            notification.NewAddress.Id,
            notification.NewAddress.Address.Street1,
            notification.NewAddress.Address.Street2,
            notification.NewAddress.Address.City,
            notification.NewAddress.Address.State,
            notification.NewAddress.Address.PostalCode,
            notification.NewAddress.Address.Country);

        await mediator.Publish(new AddressAddedIntegrationEvent(addressDetails), ct);
    }
}
