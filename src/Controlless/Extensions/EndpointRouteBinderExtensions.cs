using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class EndpointRouteBinderExtensions
    {
        public static IEndpointConventionBuilder MapControlless(
           this IEndpointRouteBuilder endpoints)
        {
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));

            return endpoints.MapGet("/{*name}", async context =>
            {
                var processor = new RequestProcessor(context);

                var binders = context.RequestServices.GetRequiredService<IEnumerable<IRequestBinder>>();
                
                foreach(var binder in binders)
                {
                    var request = binder.Bind(context.Request, context.RequestAborted);

                    if(request == null)
                        continue;
                        
                    await processor.ProcessRequest(request);

                    return;
                }

                // TODO handle no matched request
            });
        }
    }
}
