using RiverBooks.Books.Domain;
using RiverBooks.Books.Interfaces;

namespace RiverBooks.Books.UseCases;

internal class BookService(IBookRepository bookRepository) : IBookService
{
    public async Task CreateBookAsync(BookDto newBook)
    {
        var book = new Book(Guid.NewGuid(), newBook.Title, newBook.Author, newBook.Price);
        await bookRepository.AddAsync(book);
        await bookRepository.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book is not null)
        {
            await bookRepository.DeleteAsync(book);
            await bookRepository.SaveChangesAsync();
        }
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        return new BookDto(book!.Id, book.Title, book.Author, book.Price);
    }

    public async Task<List<BookDto>> ListBooksAsync()
    {
        var books = (await bookRepository.GetAllAsync())
            .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price))
            .ToList();
        return books;
    }

    public async Task UpdateBookPriceAsync(Guid id, decimal newPrice)
    {
        var book = await bookRepository.GetByIdAsync(id);
        book!.UpdatePrice(newPrice);
        await bookRepository.SaveChangesAsync();
    }
}
