using EventCachingDemo.Shared;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace EventCachingDemo.Server.Services;

public class MessageBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        using var reader = new StreamReader(bindingContext.HttpContext.Request.Body);
        var json = await reader.ReadToEndAsync();

        var contract = JsonConvert.DeserializeObject<Message>(json);

        var result = contract?.Request;

        bindingContext.Result = ModelBindingResult.Success(result);
    }
}