using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing.Data;

internal class OrderRepository(OrderProcessingDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
    }

    public async Task<List<Order>> ListAsync()
    {
        return await dbContext.Orders.Include(x => x.OrderItems).ToListAsync();
    }

    public Task SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}
