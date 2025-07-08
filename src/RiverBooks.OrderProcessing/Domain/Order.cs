using Ardalis.GuardClauses;
using RiverBooks.SharedKernel;

namespace RiverBooks.OrderProcessing.Domain;

internal class Order : IHaveDomainEvents
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;
    private List<DomainEventBase> _domainEvents = new();
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    private void AddOrderItem(OrderItem item)
    {
        _orderItems.Add(item);
    }

    private void RegisterDomainEvent(OrderCreatedEvent domainEvent)
    {
        Guard.Against.Null(domainEvent);
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    internal class Factory
    {
        public static Order Create(
            Guid userId,
            Address shippingAddress,
            Address billingAddress,
            IEnumerable<OrderItem> orderItems)
        {
            var order = new Order();
            order.UserId = userId;
            order.ShippingAddress = shippingAddress;
            order.BillingAddress = billingAddress;
            foreach (var item in orderItems)
                order.AddOrderItem(item);
            var orderCreatedEvent = new OrderCreatedEvent(order);
            order.RegisterDomainEvent(orderCreatedEvent);
            return order;
        }
    }
}
