using System.ComponentModel.DataAnnotations;
using MediatR;

namespace EventCachingDemo.Shared.Commands;

public class AddProductCommand : IRequest
{
    public AddProductCommand()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
    }

    [Required] [StringLength(50)] public string Name { get; set; }

    [Required] [StringLength(250)] public string Description { get; set; }

    [Required] [Range(0, 99999.99)] public decimal Price { get; set; }
}