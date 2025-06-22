using FastEndpoints;

namespace RiverBooks.Books;

internal class CreateBookEndpoint(IBookService bookService) : Endpoint<CreateBookRequest, BookDto>
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
        await SendCreatedAtAsync<GetBookByIdEndpoint>(new { newBook.Id }, newBook, cancellation: ct);
    }
}
