using Ardalis.Result;

namespace RiverBooks.OrderProcessing;

internal interface IAddressCache
{
    Task<Result<Address>> GetByIdAsync(Guid addressId);
}
