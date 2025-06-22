namespace RiverBooks.Books;

public class CreateBookRequest
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required decimal Price { get; set; }
}
