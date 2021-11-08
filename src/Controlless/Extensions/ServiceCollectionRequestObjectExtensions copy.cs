using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class ServiceCollectionExtensions
    {
        public static void AddControllessRequests(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddControllessRequestObjects();
            services.AddControllessRequestHandlers(Assembly.GetCallingAssembly());
        }
    }
}
