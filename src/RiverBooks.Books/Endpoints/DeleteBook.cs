using FastEndpoints;
using FluentValidation;
using RiverBooks.Books.Interfaces;

namespace RiverBooks.Books.Endpoints;

public record DeleteBookRequest(Guid Id);

public class DeleteBookRequestValidator : Validator<DeleteBookRequest>
{
    public DeleteBookRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}

internal class DeleteBook(IBookService bookService) : Endpoint<DeleteBookRequest>
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
