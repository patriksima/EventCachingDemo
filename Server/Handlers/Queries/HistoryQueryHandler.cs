using System.Reflection;
using System.Text;
using EventCachingDemo.Shared.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EventCachingDemo.Server.Handlers.Queries;

public class HistoryQueryHandler : IRequestHandler<HistoryQuery, IList<HistoryDto>>
{
    private readonly MyContext _dbContext;

    public HistoryQueryHandler(MyContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IList<HistoryDto>> Handle(HistoryQuery request, CancellationToken cancellationToken)
    {
        var results = new List<HistoryDto>();
        
        var history = await _dbContext.History
            .Select(e => new HistoryDto
            {
                HistoryId = e.HistoryId,
                RequestName = e.RequestName,
                RequestBody = e.RequestBody,
                Created = e.Created
            })
            .AsNoTracking()
            .OrderByDescending(e => e.Created)
            .ToListAsync(cancellationToken: cancellationToken);

        foreach (var dto in history)
        {
            var dtoParams = new StringBuilder();

            var dtoRequest =
                (IBaseRequest)JsonConvert.DeserializeObject(dto.RequestBody, Type.GetType(dto.RequestName)!)!;

            var properties = dtoRequest.GetType().GetProperties();
            
            foreach (var property in properties)
            {
                dtoParams.Append(property.Name);
                dtoParams.Append('=');
                dtoParams.Append(property.GetValue(dtoRequest));
                dtoParams.Append("; ");
            }
            
            results.Add(new HistoryDto
            {
                HistoryId = dto.HistoryId,
                RequestName = Type.GetType(dto.RequestName)!.Name,
                RequestBody = dtoParams.ToString(),
                Created = dto.Created
            });
        }

        return results;
    }
}