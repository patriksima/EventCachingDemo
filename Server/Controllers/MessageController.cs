using EventCachingDemo.Client.Pages;
using EventCachingDemo.Shared.Events;
using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventCachingDemo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;

    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> Post(IBaseRequest request)
    {
        var result = await _mediator.Send(request);
        if (request is not HistoryQuery)
        {
            await _mediator.Publish(new HistoryChangedEvent(request));
        }

        return new JsonResult(result);
    }
}