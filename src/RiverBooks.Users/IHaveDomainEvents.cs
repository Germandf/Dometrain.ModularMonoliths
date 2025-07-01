namespace RiverBooks.Users;

public interface IHaveDomainEvents
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
    void ClearDomainEvents();
}
