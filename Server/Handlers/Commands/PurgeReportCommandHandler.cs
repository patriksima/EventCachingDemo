using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Commands;

public class PurgeReportCommandHandler : IRequestHandler<PurgeReportCommand>
{
    private readonly MyContext _dbContext;

    public PurgeReportCommandHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(PurgeReportCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE [{nameof(Report)}s]",
            cancellationToken: cancellationToken);

        return Unit.Value;
    }
}