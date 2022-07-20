using EventCachingDemo.Server.Models;
using EventCachingDemo.Shared.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventCachingDemo.Server.Handlers.Commands;

public class PurgeLogCommandHandler : IRequestHandler<PurgeLogCommand>
{
    private readonly MyContext _dbContext;

    public PurgeLogCommandHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(PurgeLogCommand request, CancellationToken cancellationToken)
    {
        await _dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE [{nameof(SalesLog)}s]",
            cancellationToken);

        return Unit.Value;
    }
}