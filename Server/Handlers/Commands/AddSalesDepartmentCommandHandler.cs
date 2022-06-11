using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using MediatR;

namespace EventCachingDemo.Server.Handlers.Commands;

public class AddSalesDepartmentCommandHandler : IRequestHandler<AddSalesDepartmentCommand>
{
    private readonly MyContext _dbContext;

    public AddSalesDepartmentCommandHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddSalesDepartmentCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.SalesDepartments.AddAsync(new SalesDepartment
        {
            Name = request.Name,
            Description = request.Description
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}