using Ardalis.Result;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace RiverBooks.OrderProcessing.Infrastructure;

internal class RedisAddressCache : IAddressCache
{
    private readonly IDatabase _db;

    public RedisAddressCache()
    {
        var redis = ConnectionMultiplexer.Connect("localhost");
        _db = redis.GetDatabase();
    }

    public async Task<Result<Address>> GetByIdAsync(Guid addressId)
    {
        var addressJson = await _db.StringGetAsync(addressId.ToString());
        if (addressJson.IsNullOrEmpty)
            return Result.NotFound();
        var address = JsonSerializer.Deserialize<Address>(addressJson!);
        if (address is null)
            return Result.NotFound();
        return Result.Success(address);
    }

    public async Task<Result> StoreAsync(Guid addressId, Address address)
    {
        var addressJson = JsonSerializer.Serialize(address);
        var success = await _db.StringSetAsync(addressId.ToString(), addressJson);
        if (success)
            return Result.Success();
        return Result.Error();
    }
}
