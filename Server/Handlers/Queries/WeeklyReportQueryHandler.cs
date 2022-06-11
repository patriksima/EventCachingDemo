using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Queries;

public class WeeklyReportQueryHandler : IRequestHandler<WeeklyReportQuery, IList<ReportDto>>
{
    private readonly MyContext _dbContext;

    public WeeklyReportQueryHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<ReportDto>> Handle(WeeklyReportQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Reports
            .Select(e => new ReportDto
            {
                ReportId = e.ReportId,
                Agent = e.SalesAgent.FirstName + " " + e.SalesAgent.LastName,
                MostSoldProduct = e.MostSoldProduct,
                TotalPrice = e.TotalPrice,
                TotalProducts = e.TotalProducts,
                Week = e.Week,
                Year = e.Year
            })
            .AsNoTracking()
            .OrderByDescending(e => e.Year)
            .ThenByDescending(e => e.Week)
            .ThenByDescending(e => e.TotalPrice)
            .ThenByDescending(e => e.TotalProducts)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}