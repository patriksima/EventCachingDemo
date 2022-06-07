using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Models;

public class SalesDepartment
{
    public Guid SalesDepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<SalesAgent> SalesAgents { get; set; }
}

public class SalesAgent
{
    public Guid SalesAgentId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    // reference to parent department
    public Guid SalesDepartmentId { get; set; }
    public SalesDepartment SalesDepartment { get; set; }
}

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Precision(14, 2)] public decimal Price { get; set; }
}

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