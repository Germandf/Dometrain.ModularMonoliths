using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

internal interface IReadOnlyUserAddressRepository
{
    Task<UserAddress?> GetByIdAsync(Guid addressId);
}
