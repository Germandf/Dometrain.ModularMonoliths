namespace RiverBooks.Users;

internal interface IReadOnlyUserAddressRepository
{
    Task<UserAddress?> GetByIdAsync(Guid addressId);
}
