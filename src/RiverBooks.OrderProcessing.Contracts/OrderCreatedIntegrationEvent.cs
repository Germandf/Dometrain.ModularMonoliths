using MediatR;

namespace RiverBooks.OrderProcessing.Contracts;

public record OrderCreatedIntegrationEvent(OrderDetail OrderDetail) : INotification;
