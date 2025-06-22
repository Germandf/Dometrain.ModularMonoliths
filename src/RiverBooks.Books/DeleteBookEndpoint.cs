using FastEndpoints;

namespace RiverBooks.Books;

internal class DeleteBookEndpoint(IBookService bookService) : Endpoint<DeleteBookRequest>
{
    public override void Configure()
    {
        Delete("/books/{Id}");
        AllowAnonymous();
    }
    public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
    {
        await bookService.DeleteBookAsync(req.Id);
        await SendNoContentAsync(ct);
    }
}
