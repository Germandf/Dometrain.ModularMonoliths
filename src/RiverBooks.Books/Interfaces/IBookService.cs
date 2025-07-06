using RiverBooks.Books.UseCases;

namespace RiverBooks.Books.Interfaces;

internal interface IBookService
{
    Task<List<BookDto>> ListBooksAsync();
    Task<BookDto> GetBookByIdAsync(Guid id);
    Task CreateBookAsync(BookDto newBook);
    Task DeleteBookAsync(Guid id);
    Task UpdateBookPriceAsync(Guid id, decimal newPrice);
}
