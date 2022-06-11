using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Queries;

public class SalesAgentsQueryHandler : IRequestHandler<SalesAgentsQuery, IList<SalesAgentDto>>
{
    private readonly MyContext _dbContext;

    public SalesAgentsQueryHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IList<SalesAgentDto>> Handle(SalesAgentsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.SalesAgents
            .Select(e => new SalesAgentDto()
            {
                SalesAgentId = e.SalesAgentId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Department = e.SalesDepartment.Name
            })
            .AsNoTracking()
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}