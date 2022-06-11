using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Precision(14, 2)] public decimal Price { get; set; }
}