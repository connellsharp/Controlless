using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Uncontrollable
{
    public static class EndpointRouteBinderExtensions
    {
        public static IEndpointConventionBuilder MapUncontrollable(
           this IEndpointRouteBuilder endpoints)
        {
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));

            return endpoints.MapGet("/{*name}", async context =>
            {
                var binders = context.RequestServices.GetRequiredService<IEnumerable<IRequestBinder>>();
                
                foreach(var binder in binders)
                {
                    var request = binder.Bind(context.Request, context.RequestAborted);

                    if(request == null)
                        continue;
                    
                    var handlerType = typeof(WeakRequestHandler<>).MakeGenericType(request.GetType());
                    var handler = (IWeakRequestHandler)context.RequestServices.GetRequiredService(handlerType);

                    var response = await handler.Handle(request, context.RequestAborted);

                    var writer = context.RequestServices.GetRequiredService<IResponseWriter<object>>();
                    // TODO use custom writers per type
                    await writer.Write(response, context.Response);

                    return;
                }

                // TODO handle no matched request
            });
        }
    }
}
