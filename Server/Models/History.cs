using System.ComponentModel.DataAnnotations.Schema;

namespace EventCachingDemo.Server.Models;

public class History
{
    public Guid HistoryId { get; set; }
    public string RequestName { get; init; } = default!;
    [Column(TypeName = "ntext")]
    public string RequestBody { get; init; } = default!;
    public DateTimeOffset Created { get; init; } = default!;
}