using EventCachingDemo.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<SalesDepartment> SalesDepartments { get; set; }
    public DbSet<SalesAgent> SalesAgents { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<SalesLog> SalesLogs { get; set; }
    public DbSet<Report> Reports { get; set; }
}