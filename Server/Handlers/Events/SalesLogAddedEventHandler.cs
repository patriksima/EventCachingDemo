using Ardalis.GuardClauses;
using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
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

        var requestWeek = request.DateOfSale.GetWeek();
        var requestYear = request.DateOfSale.Year;

        var reportWeek = await _dbContext
            .Reports
            .Where(e => e.Week == requestWeek && e.Year == requestYear)
            .FirstOrDefaultAsync(cancellationToken);

        var requestAgent = _dbContext.SalesAgents.SingleOrDefault(e => e.SalesAgentId == request.SalesAgentId);
        Guard.Against.Null(requestAgent, nameof(requestAgent), "Sales agent does not exist.");

        var requestProduct = _dbContext.Products.SingleOrDefault(e => e.ProductId == request.ProductId);
        Guard.Against.Null(requestProduct, nameof(requestProduct), "Product does not exist.");

        // report for this week does not exist, so create the first one!
        if (reportWeek is null)
        {
            await CreateReport(request, requestWeek, requestYear, requestAgent, requestProduct, cancellationToken);
            return;
        }

        // check if new log is better than winner of the week
        if (reportWeek.TotalPrice < request.Price)
        {
            // update report with new max
            await UpdateReport(reportWeek, requestAgent.FirstName + " " + requestAgent.LastName,
                requestAgent.SalesAgentId, requestProduct.Name, request.Quantity, request.Price, cancellationToken);
            return;
        }

        // the worst case scenario - find the winner
        var winner = await GetBestSalesmanOfWeek(requestWeek, requestYear, cancellationToken);

        // update week report
        await UpdateReport(reportWeek, winner.Agent, winner.AgentId, winner.Product, winner.TotalProducts,
            winner.TotalPrice,
            cancellationToken);
    }

    /// <summary>
    ///     Finds the best salesman of the week by total amount sold, and by number of units sold
    /// </summary>
    /// <param name="requestWeek"></param>
    /// <param name="requestYear"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<WinnerDto> GetBestSalesmanOfWeek(int requestWeek, int requestYear,
        CancellationToken cancellationToken)
    {
        var firstMonday = DateTime.Now.GetFirstMondayOfYear().DayOfYear;

        var salesLog = await _dbContext
            .SalesLogs
            .Where(e =>
                requestWeek ==
                (int)Math.Floor((double)(e.DayOfSale.DayOfYear - firstMonday) / 7) + 2 // TODO: expression
                && requestYear == e.DayOfSale.Year)
            .Select(e => new
            {
                AgentId = e.SalesAgentId,
                Agent = e.SalesAgent.FirstName + " " + e.SalesAgent.LastName,
                Product = e.Product.Name,
                e.Quantity,
                e.Price
            })
            .ToListAsync(cancellationToken);

        // pick winner and save
        var winner = salesLog
            .GroupBy(e => new { e.AgentId, e.Agent, e.Product })
            .Select(e => new WinnerDto
            {
                AgentId = e.Key.AgentId,
                Agent = e.Key.Agent,
                Product = e.Key.Product,
                TotalPrice = e.Sum(x => x.Price),
                TotalProducts = e.Sum(x => x.Quantity)
            })
            .OrderByDescending(e => e.TotalPrice)
            .ThenByDescending(e => e.TotalProducts)
            .First();

        return winner;
    }

    /// <summary>
    ///     Update report for the week/year
    /// </summary>
    /// <param name="report"></param>
    /// <param name="agent"></param>
    /// <param name="agentId"></param>
    /// <param name="mostSoldProduct"></param>
    /// <param name="totalProducts"></param>
    /// <param name="totalPrice"></param>
    /// <param name="cancellationToken"></param>
    private async Task UpdateReport(Report report, string agent, Guid agentId, string mostSoldProduct,
        int totalProducts, decimal totalPrice, CancellationToken cancellationToken)
    {
        // update week report
        report.Agent = agent;
        report.SalesAgentId = agentId;
        report.MostSoldProduct = mostSoldProduct;
        report.TotalProducts = totalProducts;
        report.TotalPrice = totalPrice;

        // save changes
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    ///     Create new report for the week/year
    /// </summary>
    /// <param name="request"></param>
    /// <param name="requestWeek"></param>
    /// <param name="requestYear"></param>
    /// <param name="requestAgent"></param>
    /// <param name="requestProduct"></param>
    /// <param name="cancellationToken"></param>
    private async Task CreateReport(AddSalesLogCommand request, int requestWeek, int requestYear,
        SalesAgent requestAgent, Product requestProduct, CancellationToken cancellationToken)
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
    }
}

internal record WinnerDto
{
    public Guid AgentId { get; init; }
    public string Agent { get; init; } = default!;
    public string Product { get; init; } = default!;
    public decimal TotalPrice { get; init; }
    public int TotalProducts { get; init; }
}