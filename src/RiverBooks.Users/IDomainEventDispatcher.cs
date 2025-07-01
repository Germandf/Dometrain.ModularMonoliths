namespace RiverBooks.Users;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEventsAsync(IEnumerable<IHaveDomainEvents> entities, CancellationToken cancellationToken = default);
}
