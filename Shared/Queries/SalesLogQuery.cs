using MediatR;

namespace EventCachingDemo.Shared.Queries;

public class SalesLogQuery : IRequest<IList<SalesLogDto>>
{
    
}

public record SalesLogDto
{
    public Guid SalesLogId { get; init; } = default!;
    public int Quantity { get; init; } = default!;
    public decimal Price { get; init; } = default!;
    public DateTimeOffset DayOfSale { get; init; } = default!;
    public string SalesAgent { get; init; } = default!;
    public string Product { get; init; } = default!;
}