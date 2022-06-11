using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Models;

[Index(nameof(Year), nameof(Week))]
public class Report
{
    public Guid ReportId { get; set; }
    public int Year { get; set; }
    public int Week { get; set; }
    public string Agent { get; set; }
    public int TotalProducts { get; set; }
    [Precision(14, 2)] public decimal TotalPrice { get; set; }

    public string MostSoldProduct { get; set; }

    // reference to agent
    public Guid SalesAgentId { get; set; }
    public SalesAgent SalesAgent { get; set; }
}