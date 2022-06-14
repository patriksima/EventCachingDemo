using MediatR;

namespace EventCachingDemo.Shared.Events;

public record HistoryChangedEvent(IBaseRequest Request) : INotification;