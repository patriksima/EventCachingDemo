using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Events;
using MediatR;
using Newtonsoft.Json;

namespace EventCachingDemo.Server.Handlers.Events;

public class HistoryChangedEventHandler : INotificationHandler<HistoryChangedEvent>
{
    private readonly MyContext _dbContext;

    public HistoryChangedEventHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(HistoryChangedEvent notification, CancellationToken cancellationToken)
    {
        await _dbContext.History.AddAsync(new History
        {
            RequestName = notification.Request.GetType().AssemblyQualifiedName!,
            RequestBody = JsonConvert.SerializeObject(notification.Request),
            Created = DateTimeOffset.Now
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}