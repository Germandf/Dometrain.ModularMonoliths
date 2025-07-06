using MediatR;

namespace RiverBooks.SharedKernel;

public class MediatRDomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchAndClearEventsAsync(IEnumerable<IHaveDomainEvents> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.DomainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
            entity.ClearDomainEvents();
        }
    }
}
