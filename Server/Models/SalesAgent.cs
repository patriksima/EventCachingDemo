namespace EventCachingDemo.Server.Models;

public class SalesAgent
{
    public Guid SalesAgentId { get; set; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;

    // reference to parent department
    public Guid SalesDepartmentId { get; set; }
    public SalesDepartment SalesDepartment { get; set; } = default!;
}