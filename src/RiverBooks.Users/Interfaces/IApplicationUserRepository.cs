using RiverBooks.Users.Domain;

namespace RiverBooks.Users.Interfaces;

internal interface IApplicationUserRepository
{
    Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
    Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
    Task SaveChangesAsync();
}
