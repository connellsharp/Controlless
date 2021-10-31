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
                var finders = context.RequestServices.GetRequiredService<IEnumerable<IRequestFinder>>();
                
                foreach(var finder in finders)
                {
                    var matchedRequest = finder.Find(context);

                    if(matchedRequest.IsMatched == false)
                        continue;
                    
                    var handlerType = typeof(WeakRequestHandler<>).MakeGenericType(matchedRequest.RequestType);
                    var handler = (IWeakRequestHandler)context.RequestServices.GetRequiredService(handlerType);

                    await handler.Handle(matchedRequest.RequestObject, context.Response);
                    return;
                }

                // TODO handle no matched request
            });
        }
    }
}
