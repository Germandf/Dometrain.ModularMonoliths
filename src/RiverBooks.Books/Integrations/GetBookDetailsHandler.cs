using Ardalis.Result;
using MediatR;
using RiverBooks.Books.Contracts;

namespace RiverBooks.Books.Integrations;

internal class GetBookDetailsHandler(IBookService bookService) : IRequestHandler<GetBookDetailsQuery, Result<BookDetails>>
{
    public async Task<Result<BookDetails>> Handle(GetBookDetailsQuery request, CancellationToken cancellationToken)
    {
        var book = await bookService.GetBookByIdAsync(request.BookId);
        if (book is null)
            return Result.NotFound();

        return new BookDetails(book.Id, book.Title, book.Author, book.Price);
    }
}
