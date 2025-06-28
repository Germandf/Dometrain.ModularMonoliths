namespace RiverBooks.Users;

internal interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
    Task SaveChangesAsync();
}
