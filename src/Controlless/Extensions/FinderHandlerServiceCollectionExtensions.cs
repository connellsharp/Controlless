using System;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class FinderHandlerServiceCollectionExtensions
    {
        public static void AddControlless(
           this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton(typeof(RequestHandlerWeakAdapter<>));
            services.AddSingleton(typeof(ResponseWriterWeakAdapter<>));
            services.AddSingleton(typeof(IResponseWriter<>), typeof(JsonResponseWriter<>));
        }
    }
}
