using System.ComponentModel.DataAnnotations;
using MediatR;

namespace EventCachingDemo.Shared.Commands;

public class AddSalesAgentCommand : IRequest
{
    public AddSalesAgentCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        SalesDepartmentId = Guid.Empty;
    }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50)]
    public string LastName { get; set; }
    [Required]
    public Guid SalesDepartmentId { get; set; }
}