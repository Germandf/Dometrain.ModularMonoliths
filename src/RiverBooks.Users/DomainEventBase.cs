using MediatR;

namespace RiverBooks.Users;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOcurred { get; protected set; } = DateTime.UtcNow;
}
