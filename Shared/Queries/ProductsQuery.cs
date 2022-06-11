using MediatR;

namespace EventCachingDemo.Shared.Queries;

public class ProductsQuery : IRequest<IList<ProductDto>>
{
}

public record ProductDto
{
    public Guid ProductId { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public decimal Price { get; init; } = default!;
}