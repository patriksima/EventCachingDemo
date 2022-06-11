using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using MediatR;

namespace EventCachingDemo.Server.Handlers.Commands;

public class AddSalesAgentCommandHandler : IRequestHandler<AddSalesAgentCommand>
{
    private readonly MyContext _dbContext;

    public AddSalesAgentCommandHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddSalesAgentCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.SalesAgents.AddAsync(new SalesAgent
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            SalesDepartmentId = request.SalesDepartmentId
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}