using MediatR;

namespace EventCachingDemo.Shared.Queries;

public class WeeklyReportQuery : IRequest<IList<ReportDto>>
{
}

public record ReportDto
{
    public Guid ReportId { get; init; } = default!;
    public int Year { get; init; } = default!;
    public int Week { get; init; } = default!;
    public int TotalProducts { get; init; } = default!;
    public decimal TotalPrice { get; init; } = default!;
    public string Agent { get; init; } = default!;
    public string MostSoldProduct { get; init; } = default!;
}