using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class EndpointRouteBinderExtensions
    {
        public static void MapBinders(
           this IEndpointRouteBuilder endpoints)
        {
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));

            var binders = endpoints.ServiceProvider.GetRequiredService<IEnumerable<IRequestBinder>>();

            foreach(var binder in binders)
            {
                endpoints.MapBinder(binder);
            }

            // TODO return composite ConventionBuilder
        }

        public static IEndpointConventionBuilder MapBinder<TBinder>(
           this IEndpointRouteBuilder endpoints)
            where TBinder : IRequestBinder
        {
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));

            var binder = endpoints.ServiceProvider.GetRequiredService<TBinder>();
            return endpoints.MapBinder(binder);
        }

        public static IEndpointConventionBuilder MapBinder(
           this IEndpointRouteBuilder endpoints,
           IRequestBinder binder)
        {
            if (endpoints == null)
                throw new ArgumentNullException(nameof(endpoints));

            if (binder == null)
                throw new ArgumentNullException(nameof(binder));

            return endpoints.MapMethods(binder.Route, new[] { binder.Method }, async context =>
            {
                var request = binder.Bind(context.Request, context.RequestAborted);
                        
                var processor = new RequestProcessor(context);
                await processor.ProcessRequest(request);
            });
        }
    }
}
