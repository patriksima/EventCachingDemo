namespace EventCachingDemo.Server.Models;

public class SalesDepartment
{
    public Guid SalesDepartmentId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public virtual ICollection<SalesAgent> SalesAgents { get; set; } = default!;
}