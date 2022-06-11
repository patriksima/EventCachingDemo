namespace EventCachingDemo.Server.Models;

public class SalesAgent
{
    public Guid SalesAgentId { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    // reference to parent department
    public Guid SalesDepartmentId { get; set; }
    public SalesDepartment SalesDepartment { get; set; }
}