using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using EventCachingDemo.Shared.Events;
using MediatR;

namespace EventCachingDemo.Server.Handlers.Commands;

public class AddSalesLogCommandHandler : IRequestHandler<AddSalesLogCommand>
{
    private readonly MyContext _dbContext;
    private readonly IMediator _mediator;

    public AddSalesLogCommandHandler(MyContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
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

        var written = await _dbContext.SaveChangesAsync(cancellationToken);

        if (written > 0)
        {
            await _mediator.Publish(new SalesLogAddedEvent(request), cancellationToken);
        }

        return Unit.Value;
    }
}