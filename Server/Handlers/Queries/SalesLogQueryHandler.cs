using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Queries;

public class SalesLogQueryHandler : IRequestHandler<SalesLogQuery, IList<SalesLogDto>>
{
    private readonly MyContext _dbContext;

    public SalesLogQueryHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<SalesLogDto>> Handle(SalesLogQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.SalesLogs
            .Select(e => new SalesLogDto
            {
                SalesLogId = e.SalesLogId,
                Quantity = e.Quantity,
                Price = e.Price,
                DayOfSale = e.DayOfSale,
                SalesAgent = e.SalesAgent.FirstName + " " + e.SalesAgent.LastName,
                Product = e.Product.Name
            })
            .AsNoTracking()
            .OrderByDescending(e => e.DayOfSale)
            .ToListAsync(cancellationToken);
    }
}