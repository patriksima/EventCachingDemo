using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;

    [Precision(14, 2)] public decimal Price { get; init; }
}