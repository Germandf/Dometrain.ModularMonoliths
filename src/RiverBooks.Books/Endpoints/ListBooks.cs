using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public record ListBooksResponse(List<BookDto> Books);

internal class ListBooks(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
{
    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var books = await bookService.ListBooksAsync();
        await SendAsync(new ListBooksResponse(books));
    }
}
