using FastEndpoints;

namespace RiverBooks.Books;

internal class UpdateBookPriceEndpoint(IBookService bookService) : Endpoint<UpdateBookPriceRequest, BookDto>
{
    public override void Configure()
    {
        Post("/books/{Id}/pricehistory");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
    {
        await bookService.UpdateBookPriceAsync(req.Id, req.NewPrice);
        var updatedBook = await bookService.GetBookByIdAsync(req.Id);
        await SendAsync(updatedBook, cancellation: ct);
    }
}
