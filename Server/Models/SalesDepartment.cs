namespace EventCachingDemo.Server.Models;

public class SalesDepartment
{
    public Guid SalesDepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<SalesAgent> SalesAgents { get; set; }
}