using Bogus;
using EventCachingDemo.Server.Helpers;
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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // generate departments
        var department = new Faker<SalesDepartment>()
            .RuleFor(m => m.SalesDepartmentId, f => f.Random.Guid())
            .RuleFor(m => m.Name, f => f.Commerce.Department())
            .RuleFor(m => m.Description, f => f.Lorem.Sentence(3));

        var departments = department.GenerateBetween(5, 10);
        
        modelBuilder
            .Entity<SalesDepartment>()
            .HasData(departments);

        // generate agents
        var agent = new Faker<SalesAgent>()
            .RuleFor(e => e.SalesAgentId, f => f.Random.Guid())
            .RuleFor(e => e.FirstName, f => f.Person.FirstName)
            .RuleFor(e => e.LastName, f => f.Person.LastName)
            .RuleFor(e => e.SalesDepartmentId, f => departments.GetRandomElement()!.SalesDepartmentId);
        
        var agents = agent.GenerateBetween(5, 10);
        
        modelBuilder
            .Entity<SalesAgent>()
            .HasData(agents);
        
        // generate products
        var product = new Faker<Product>()
            .RuleFor(e => e.ProductId, f => f.Random.Guid())
            .RuleFor(e => e.Name, f => f.Commerce.ProductName())
            .RuleFor(e => e.Description, f => f.Commerce.ProductDescription())
            .RuleFor(e => e.Price, f => f.Finance.Amount());
        
        var products = product.GenerateBetween(5, 10);
        
        modelBuilder
            .Entity<Product>()
            .HasData(products);
    }
}