﻿namespace RiverBooks.OrderProcessing;

internal class Order
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid UserId { get; private set; }
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;

    private void AddOrderItem(OrderItem item)
    {
        _orderItems.Add(item);
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
            return order;
        }
    }
}
