using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMvcAndControlless(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddMvc().AddControllessRequests();
            services.AddControllessHandlers(Assembly.GetCallingAssembly());
        }
    }
}
