using MediatR;

namespace EventCachingDemo.Shared.Queries;

public class SalesDepartmentsQuery : IRequest<IList<SalesDepartmentDto>>
{
}

public record SalesDepartmentDto
{
    public Guid SalesDepartmentId { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public IReadOnlyCollection<string> SalesAgents { get; init; } = default!;
}