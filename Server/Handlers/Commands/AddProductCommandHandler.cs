using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using MediatR;

namespace EventCachingDemo.Server.Handlers.Commands;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
{
    private readonly MyContext _dbContext;

    public AddProductCommandHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Products.AddAsync(new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}