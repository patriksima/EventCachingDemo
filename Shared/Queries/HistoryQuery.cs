using MediatR;

namespace EventCachingDemo.Shared.Queries;

public class HistoryQuery : IRequest<IList<HistoryDto>>
{
}

public class HistoryDto
{
    public Guid HistoryId { get; init; }
    public string RequestName { get; init; } = default!;
    public string RequestBody { get; init; } = default!;
    public DateTimeOffset Created { get; init; } = default!;
}