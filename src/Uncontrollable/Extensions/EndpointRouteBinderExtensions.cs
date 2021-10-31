using System;
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
                var finder = context.RequestServices.GetRequiredService<IRequestFinder>();
                
                var request = finder.Find(context);
                
                var handler = (IWeakRequestHandler)context.RequestServices.GetRequiredService(typeof(WeakRequestHandler<>).MakeGenericType(request.GetType()));

                await handler.Handle(request, context.Response);
            });
        }
    }
}
