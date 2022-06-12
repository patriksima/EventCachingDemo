using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Helpers;
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
        /*
        var firstMonday = DateTime.Now.GetFirstMondayOfYear().DayOfYear;
        
        var reports = (await _dbContext
            .SalesLogs
            .GroupBy(e => new
            {
                Year = e.DayOfSale.Year,
                Week = (int)Math.Floor((double)(e.DayOfSale.DayOfYear - firstMonday) / 7) + 2,
                Agent = e.SalesAgent.FirstName + " " + e.SalesAgent.LastName,
                //AgentId = e.SalesAgentId,
                Product = e.Product.Name
            })
            .Select(e => new
            {
                e.Key.Year,
                e.Key.Week,
                e.Key.Agent,
                //e.Key.AgentId,
                e.Key.Product,
                TotalProducts = e.Sum(x => x.Quantity),
                TotalPrice = e.Sum(x => x.Price)
            })
            .OrderByDescending(e => e.Year)
            .ThenByDescending(e => e.Week)
            .ThenByDescending(e => e.TotalPrice)
            .ThenByDescending(e => e.TotalProducts)
            .ToListAsync(cancellationToken: cancellationToken))
            
            .GroupBy(e => new { e.Year, e.Week })
            .Select(e => new ReportDto
            {
                Year=e.Key.Year,
                Week=e.Key.Week,
                //AgentId = e.Select(x => x.AgentId).First(),
                Agent = e.Select(x => x.Agent).First(),
                MostSoldProduct = e.Select(x => x.Product).First(),
                TotalProducts = e.Max(x => x.TotalProducts),
                TotalPrice = e.Max(x => x.TotalPrice)
            })

            .OrderByDescending(e => e.Year)
            .ThenByDescending(e => e.Week)
            .ThenByDescending(e => e.TotalPrice)
            .ThenByDescending(e => e.TotalProducts)
            .ToList();

        foreach (var report in reports)
        {
            var agentId = _dbContext.SalesAgents.Where(e => e.FirstName + " " + e.LastName == report.Agent)
                .Select(e => e.SalesAgentId).FirstOrDefault();
            
            var entity = new Report()
            {
                Year = report.Year,
                Week = report.Week,
                Agent = report.Agent,
                MostSoldProduct = report.MostSoldProduct,
                TotalPrice = report.TotalPrice,
                TotalProducts = report.TotalProducts,
                SalesAgentId = agentId
            };

            await _dbContext.Reports.AddAsync(entity, cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return reports;
        */
        return await _dbContext.Reports
            .Select(e => new ReportDto
            {
                ReportId = e.ReportId,
                Year = e.Year,
                Week = e.Week,
                Agent = e.SalesAgent.FirstName + " " + e.SalesAgent.LastName,
                MostSoldProduct = e.MostSoldProduct,
                TotalPrice = e.TotalPrice,
                TotalProducts = e.TotalProducts
            })
            .AsNoTracking()
            .OrderByDescending(e => e.Year)
            .ThenByDescending(e => e.Week)
            .ThenByDescending(e => e.TotalPrice)
            .ThenByDescending(e => e.TotalProducts)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}