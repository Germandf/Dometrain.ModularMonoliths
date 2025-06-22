using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public record CreateBookRequest(string Title, string Author, decimal Price);

internal class CreateBook(IBookService bookService) : Endpoint<CreateBookRequest, BookDto>
{
    public override void Configure()
    {
        Post("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
    {
        var newBook = new BookDto(Guid.NewGuid(), req.Title, req.Author, req.Price);
        await bookService.CreateBookAsync(newBook);
        await SendCreatedAtAsync<GetBookById>(new { newBook.Id }, newBook, cancellation: ct);
    }
}
