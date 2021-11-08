using System;
using Controlless.Binding;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class ServiceCollectionRequestObjectExtensions
    {
        public static void AddControllessRequestObjects(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.
                AddMvc(o => o.Conventions.Add(
                    new GenericControllerRouteConvention()
                )).
                ConfigureApplicationPartManager(m => 
                    m.FeatureProviders.Add(new GenericControllerFeatureProvider()
                ));
        }
    }
}
