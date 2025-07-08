using MediatR;
using RiverBooks.EmailSending.Contracts;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.Users.Contracts;

namespace RiverBooks.Users.Integrations;

internal class OrderCreatedEventHandler(IMediator mediator) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken ct)
    {
        var userByIdQuery = new UserDetailsByIdQuery(notification.Order.UserId);
        var userResult = await mediator.Send(userByIdQuery, ct);
        if (userResult.IsSuccess is false)
            return;

        var sendEmailCommand = new SendEmailCommand
        {
            To = userResult.Value.Email,
            From = "donotreply@test.com",
            Subject = "Your River Books Purchase",
            Body =
                $"Hello {userResult.Value.Email},\n\n" +
                $"Thank you for your order #{notification.Order.Id} with River Books! We appreciate your business.\n\n" +
                $"Best regards,\n" +
                $"River Books Team"
        };
        _ = await mediator.Send(sendEmailCommand, ct);
    }
}
