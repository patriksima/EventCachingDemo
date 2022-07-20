using MediatR;
using Newtonsoft.Json;

namespace EventCachingDemo.Shared;

public class Message
{
    public Message()
    {
    }

    public Message(IBaseRequest request)
    {
        Json = JsonConvert.SerializeObject(request);
        Type = request.GetType().AssemblyQualifiedName!;
    }

    [JsonProperty] private string Json { get; set; } = string.Empty;

    [JsonProperty] private string Type { get; set; } = string.Empty;

    [JsonIgnore]
    public IBaseRequest Request => (IBaseRequest)JsonConvert.DeserializeObject(Json, System.Type.GetType(Type)!)!;
}