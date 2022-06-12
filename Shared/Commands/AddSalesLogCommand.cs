using System.ComponentModel.DataAnnotations;
using MediatR;

namespace EventCachingDemo.Shared.Commands;

public class AddSalesLogCommand : IRequest
{
    public AddSalesLogCommand()
    {
        SalesAgentId = Guid.Empty;
        ProductId = Guid.Empty;
        Quantity = default;
        Price = default;
        DateOfSale = DateTimeOffset.UtcNow.ToLocalTime();
    }

    [Required] public Guid SalesAgentId { get; set; }
    [Required] public Guid ProductId { get; set; }
    [Required] [Range(0, 1000)] public int Quantity { get; set; }
    [Required] [Range(0, 99999.99)] public decimal Price { get; set; }
    [Required] public DateTimeOffset DateOfSale { get; set; }
}