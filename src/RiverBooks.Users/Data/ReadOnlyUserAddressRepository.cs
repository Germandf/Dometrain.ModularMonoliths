using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users.Data;

internal class ReadOnlyUserAddressRepository(UsersDbContext dbContext) : IReadOnlyUserAddressRepository
{
    public Task<UserAddress?> GetByIdAsync(Guid addressId)
    {
        return dbContext.UserAddresses.SingleOrDefaultAsync(a => a.Id == addressId);
    }
}
