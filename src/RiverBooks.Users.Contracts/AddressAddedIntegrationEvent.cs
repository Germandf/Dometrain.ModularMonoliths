using RiverBooks.SharedKernel;

namespace RiverBooks.Users.Contracts;

public record AddressAddedIntegrationEvent(AddressDetails NewAddress) : IntegrationEventBase;
