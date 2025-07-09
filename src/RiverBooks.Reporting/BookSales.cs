namespace RiverBooks.Reporting;

internal class BookSales
{
    public required Guid BookId { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public required int Year { get; set; }
    public required int Month { get; set; }
    public required decimal TotalSales { get; set; }
    public required int UnitsSold { get; set; }
}
