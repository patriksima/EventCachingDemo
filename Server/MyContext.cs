using EventCachingDemo.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<SalesDepartment> SalesDepartments => Set<SalesDepartment>();
    public DbSet<SalesAgent> SalesAgents => Set<SalesAgent>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<SalesLog> SalesLogs => Set<SalesLog>();
    public DbSet<Report> Reports => Set<Report>();
}