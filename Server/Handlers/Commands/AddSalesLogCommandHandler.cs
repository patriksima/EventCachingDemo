using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using MediatR;

namespace EventCachingDemo.Server.Handlers.Commands;

public class AddSalesLogCommandHandler : IRequestHandler<AddSalesLogCommand>
{
    private readonly MyContext _dbContext;

    public AddSalesLogCommandHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(AddSalesLogCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.SalesLogs.AddAsync(new SalesLog
        {
            ProductId = request.ProductId,
            SalesAgentId = request.SalesAgentId,
            Quantity = request.Quantity,
            Price = request.Price,
            DayOfSale = request.DateOfSale
        }, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}