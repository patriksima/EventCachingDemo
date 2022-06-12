using EventCachingDemo.Shared.Commands;
using MediatR;

namespace EventCachingDemo.Shared.Events;

public record SalesLogAddedEvent(AddSalesLogCommand Request) : INotification;