using MediatR;

namespace RiverBooks.SharedKernel;

public abstract class DomainEventBase : INotification
{
    public DateTime DateOcurred { get; protected set; } = DateTime.UtcNow;
}
