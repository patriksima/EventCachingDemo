using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Models;

public class SalesLog
{
    public Guid SalesLogId { get; set; }
    public int Quantity { get; set; }
    [Precision(14, 2)] public decimal Price { get; set; }

    public DateTimeOffset DayOfSale { get; set; }

    // reference to agent
    public Guid SalesAgentId { get; set; }

    public SalesAgent SalesAgent { get; set; }

    // reference to product
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}