using System;
using System.Linq;
using System.Reflection;
using Controlless.Binding;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class RequestObjectServiceCollectionExtensions
    {
        public static void AddRequestObjects(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.
                AddMvc(o => o.Conventions.Add(
                    new GenericControllerRouteConvention()
                )).
                ConfigureApplicationPartManager(m => 
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()
                ));
        }
    }
}
