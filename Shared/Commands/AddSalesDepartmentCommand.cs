using System.ComponentModel.DataAnnotations;
using MediatR;

namespace EventCachingDemo.Shared.Commands;

public class AddSalesDepartmentCommand : IRequest
{
    public AddSalesDepartmentCommand()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(250)]
    public string Description { get; set; }
}