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
        return new JsonResult(await _mediator.Send(request));
    }
}