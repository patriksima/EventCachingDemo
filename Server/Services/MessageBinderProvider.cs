using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace EventCachingDemo.Server.Services;

public class MessageBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        Guard.Against.Null(context, nameof(context));
        Guard.Against.Null(context.Metadata, nameof(context.Metadata));

        return context.Metadata.ModelType == typeof(IBaseRequest)
            ? new BinderTypeModelBinder(typeof(MessageBinder))
            : null;
    }
}