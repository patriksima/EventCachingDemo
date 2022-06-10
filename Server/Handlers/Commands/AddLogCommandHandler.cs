using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using MediatR;

namespace EventCachingDemo.Server.Handlers.Commands;

public class AddLogCommandHandler : IRequestHandler<AddLogCommand>
{
    private readonly MyContext _dbContext;

    public AddLogCommandHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(AddLogCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.SalesLogs.AddAsync(new SalesLog
        {
            ProductId = request.ProductId,
            SalesAgentId = request.SalesAgentId,
            Quantity = request.Quantity,
            Price = request.TotalPrice,
            DayOfSale = request.DateOfSale
        }, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}