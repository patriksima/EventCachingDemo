using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Events;
using EventCachingDemo.Shared.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Events;

public class SalesLogAddedEventHandler : INotificationHandler<SalesLogAddedEvent>
{
    private readonly MyContext _dbContext;

    public SalesLogAddedEventHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(SalesLogAddedEvent notification, CancellationToken cancellationToken)
    {
        var request = notification.Request;

        var firstMonday = DateTime.Now.GetFirstMondayOfYear().DayOfYear;

        var requestWeek = (int)Math.Floor((double)(request.DateOfSale.DayOfYear - firstMonday) / 7) + 2;
        var requestYear = request.DateOfSale.Year;

        var reportWeek = await _dbContext
            .Reports
            .Where(e => e.Week == requestWeek && e.Year == requestYear)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        var requestAgent = _dbContext.SalesAgents.FirstOrDefault(e => e.SalesAgentId == request.SalesAgentId);
        var requestProduct = _dbContext.Products.FirstOrDefault(e => e.ProductId == request.ProductId);

        if (requestAgent is null)
        {
            throw new Exception("Sales agent does not exist.");
        }

        if (requestProduct is null)
        {
            throw new Exception("Product does not exist.");
        }

        // report for this week does not exist, so create the first one!
        if (reportWeek is null)
        {
            var entity = new Report
            {
                Year = requestYear,
                Week = requestWeek,
                Agent = requestAgent.FirstName + " " + requestAgent.LastName,
                SalesAgentId = request.SalesAgentId,
                MostSoldProduct = requestProduct.Name,
                TotalPrice = request.Price,
                TotalProducts = request.Quantity
            };

            await _dbContext.Reports.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        // check if new log is better than winner of the week
        if (reportWeek.TotalPrice < request.Price)
        {
            // update report with new max
            reportWeek.Agent = requestAgent.FirstName + " " + requestAgent.LastName;
            reportWeek.SalesAgentId = requestAgent.SalesAgentId;
            reportWeek.TotalPrice = request.Price;
            reportWeek.TotalProducts = request.Quantity;
            reportWeek.MostSoldProduct = requestProduct.Name;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        // get sales log for week
        var salesLog = await _dbContext
            .SalesLogs
            .Where(e => requestWeek == (int)Math.Floor((double)(e.DayOfSale.DayOfYear - firstMonday) / 7) + 2
                            && requestYear == e.DayOfSale.Year)
            .Select(e => new
            {
                AgentId = e.SalesAgentId,
                Agent = e.SalesAgent.FirstName + " " + e.SalesAgent.LastName,
                Product = e.Product.Name,
                Quantity = e.Quantity,
                Price = e.Price
            })
            .ToListAsync(cancellationToken: cancellationToken);

        // pick winner and save
        var winner = salesLog
            .GroupBy(e => new { e.AgentId, e.Agent, e.Product })
            .Select(e => new
            {
                e.Key.AgentId,
                e.Key.Agent,
                e.Key.Product,
                TotalPrice = e.Sum(x => x.Price),
                TotalProducts = e.Sum(x => x.Quantity)
            })
            .OrderByDescending(e => e.TotalPrice)
            .ThenByDescending(e => e.TotalProducts)
            .First();

        // update week report
        reportWeek.Agent = winner.Agent;
        reportWeek.SalesAgentId = winner.AgentId;
        reportWeek.MostSoldProduct = winner.Product;
        reportWeek.TotalProducts = winner.TotalProducts;
        reportWeek.TotalPrice = winner.TotalPrice;

        // save changes
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}