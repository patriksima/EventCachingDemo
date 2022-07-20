using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Queries;

public class SalesDepartmentsQueryHandler : IRequestHandler<SalesDepartmentsQuery, IList<SalesDepartmentDto>>
{
    private readonly MyContext _dbContext;

    public SalesDepartmentsQueryHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<SalesDepartmentDto>> Handle(SalesDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.SalesDepartments
            .Select(e => new SalesDepartmentDto
            {
                SalesDepartmentId = e.SalesDepartmentId,
                Name = e.Name,
                Description = e.Description,
                SalesAgents = e.SalesAgents
                    .OrderBy(x => x.LastName)
                    .Select(x => x.FirstName + " " + x.LastName)
                    .ToList()
            })
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
    }
}