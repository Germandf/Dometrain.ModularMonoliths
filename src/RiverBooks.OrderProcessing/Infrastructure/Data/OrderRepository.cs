using Microsoft.EntityFrameworkCore;
using RiverBooks.OrderProcessing.Domain;
using RiverBooks.OrderProcessing.Interfaces;

namespace RiverBooks.OrderProcessing.Infrastructure.Data;

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
