﻿using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventCachingDemo.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesLogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SalesLogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<SalesLogDto>> Get()
    {
        return await _mediator.Send(new SalesLogQuery());
    }
}