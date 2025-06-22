using FastEndpoints;
using FluentValidation;

namespace RiverBooks.Books.Endpoints;

public record CreateBookRequest(string Title, string Author, decimal Price);

public class CreateBookRequestValidator : Validator<CreateBookRequest>
{
    public CreateBookRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Author)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
    }
}

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
