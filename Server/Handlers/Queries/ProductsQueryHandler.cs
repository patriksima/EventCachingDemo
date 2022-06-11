using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Queries;

public class ProductsQueryHandler : IRequestHandler<ProductsQuery, IList<ProductDto>>
{
    private readonly MyContext _dbContext;

    public ProductsQueryHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IList<ProductDto>> Handle(ProductsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Products
            .Select(e => new ProductDto()
            {
                ProductId = e.ProductId,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price
            })
            .AsNoTracking()
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}