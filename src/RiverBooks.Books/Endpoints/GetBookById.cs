using FastEndpoints;

namespace RiverBooks.Books.Endpoints;

public record GetBookByIdRequest(Guid Id);

internal class GetBookById(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDto>
{
    public override void Configure()
    {
        Get("/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
    {
        var book = await bookService.GetBookByIdAsync(req.Id);
        if (book is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        await SendAsync(book, cancellation: ct);
    }
}
