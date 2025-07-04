﻿using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books.Data;

internal class BookRepository(BookDbContext dbContext) : IBookRepository
{
    public Task AddAsync(Book book)
    {
        dbContext.Books.Add(book);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Book book)
    {
        dbContext.Books.Remove(book);
        return Task.CompletedTask;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await dbContext.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await dbContext.Books.FindAsync(id);
    }

    public Task SaveChangesAsync()
    {
        return dbContext.SaveChangesAsync();
    }
}
