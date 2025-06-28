using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Users.Data;

internal class ApplicationUserRepository(UsersDbContext dbContext) : IApplicationUserRepository
{
    public Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email)
    {
        return dbContext.ApplicationUsers
            .Include(u => u.Addresses)
            .SingleAsync(user => user.Email == email);
    }

    public Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
    {
        return dbContext.ApplicationUsers
            .Include(u => u.CartItems)
            .SingleAsync(user => user.Email == email);
    }

    public Task SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}
