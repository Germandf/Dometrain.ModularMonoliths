using Ardalis.Result;
using MediatR;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;
using RiverBooks.Users.Contracts;

namespace RiverBooks.OrderProcessing.Infrastructure;

internal class ReadThroughAddressCache(RedisAddressCache redisAddressCache, IMediator mediator) : IAddressCache
{
    public async Task<Result<Address>> GetByIdAsync(Guid addressId)
    {
        var cachedResult = await redisAddressCache.GetByIdAsync(addressId);
        if (cachedResult.Status is ResultStatus.Ok)
            return cachedResult;

        var query = new AddressDetailsByIdQuery(addressId);
        var result = await mediator.Send(query);
        if (result.IsSuccess is false)
            return result.Map();

        var dto = result.Value;
        var address = new Address(
            dto.Street1,
            dto.Street2,
            dto.City,
            dto.State,
            dto.PostalCode,
            dto.Country);
        await redisAddressCache.StoreAsync(addressId, address);
        return address;
    }

    public Task<Result> StoreAsync(Guid addressId, Address address)
    {
        return redisAddressCache.StoreAsync(addressId, address);
    }
}
