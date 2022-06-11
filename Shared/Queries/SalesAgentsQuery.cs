using MediatR;

namespace EventCachingDemo.Shared.Queries;

public class SalesAgentsQuery : IRequest<IList<SalesAgentDto>>
{
    
}

public record SalesAgentDto
{
    public Guid SalesAgentId { get; init; } = default!;
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string FullName => FirstName + " " + LastName;
    public string Department { get; init; } = default!;
}