﻿using RiverBooks.Books.Domain;

namespace RiverBooks.Books.Interfaces;

internal interface IReadOnlyBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<List<Book>> GetAllAsync();
}
