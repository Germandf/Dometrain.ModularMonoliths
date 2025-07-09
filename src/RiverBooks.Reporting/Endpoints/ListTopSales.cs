using FastEndpoints;

namespace RiverBooks.Reporting.Endpoints;

internal record ListTopSalesRequest(int Year, int Month);

internal record ListTopSalesResponse(TopBooksByMonthReport Report);

internal class ListTopSales(IBookSalesReportService reportService)
    : Endpoint<ListTopSalesRequest, ListTopSalesResponse>
{
    public override void Configure()
    {
        Get("/top-sales");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ListTopSalesRequest req, CancellationToken ct)
    {
        var report = await reportService.GetByMonthAsync(req.Year, req.Month, ct);
        var response = new ListTopSalesResponse(report);
        await SendOkAsync(response, ct);
    }
}
