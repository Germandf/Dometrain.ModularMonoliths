using Microsoft.EntityFrameworkCore;
using RiverBooks.Users.Domain;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Infrastructure.Data;

internal class ReadOnlyUserAddressRepository(UsersDbContext dbContext) : IReadOnlyUserAddressRepository
{
    public Task<UserAddress?> GetByIdAsync(Guid addressId)
    {
        return dbContext.UserAddresses.SingleOrDefaultAsync(a => a.Id == addressId);
    }
}
