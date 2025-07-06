namespace RiverBooks.SharedKernel;

public interface IHaveDomainEvents
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
    void ClearDomainEvents();
}
