using MediatR;

namespace EventCachingDemo.Shared.Commands;

public class AddLogCommand : IRequest
{
    public Guid ProductId { get; }
    public Guid SalesAgentId { get; }
    public int Quantity { get; }
    public decimal TotalPrice { get; }
    public DateTime DateOfSale { get; }

    public AddLogCommand(Guid productId, Guid salesAgentId, int quantity, decimal totalPrice, DateTime dateOfSale)
    {
        ProductId = productId;
        SalesAgentId = salesAgentId;
        Quantity = quantity;
        TotalPrice = totalPrice;
        DateOfSale = dateOfSale;
    }
}