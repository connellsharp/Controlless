using System;
using Microsoft.Extensions.DependencyInjection;

namespace Uncontrollable
{
    public static class FinderHandlerServiceCollectionExtensions
    {
        public static void AddUncontrollable(
           this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton(typeof(RequestHandlerWeakAdapter<>));
            services.AddSingleton(typeof(IResponseWriter<>), typeof(JsonResponseWriter<>));
        }
    }
}
