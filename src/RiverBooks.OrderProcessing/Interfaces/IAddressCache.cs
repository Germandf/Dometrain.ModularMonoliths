using Ardalis.Result;
using RiverBooks.OrderProcessing.Domain;

namespace RiverBooks.OrderProcessing.Interfaces;

internal interface IAddressCache
{
    Task<Result<Address>> GetByIdAsync(Guid addressId);
    Task<Result> StoreAsync(Guid addressId, Address address);
}
