using Ardalis.Result;
using StackExchange.Redis;
using System.Text.Json;

namespace RiverBooks.OrderProcessing;

internal class AddressCache : IAddressCache
{
    private readonly IDatabase _db;

    public AddressCache()
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
}
