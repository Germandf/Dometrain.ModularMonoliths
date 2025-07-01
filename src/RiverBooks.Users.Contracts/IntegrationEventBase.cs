using MediatR;

namespace RiverBooks.Users.Contracts;

public abstract record IntegrationEventBase : INotification
{
    public DateTimeOffset DateTimeOffset { get; } = DateTimeOffset.UtcNow;
}
