using System;
using Controlless.Binding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class ServiceCollectionRequestsExtensions
    {
        public static void AddControllessRequests(this IMvcBuilder mvcBuilder)
        {
            if (mvcBuilder == null)
                throw new ArgumentNullException(nameof(mvcBuilder));

            mvcBuilder.ConfigureApplicationPartManager(m =>
            {
                m.FeatureProviders.Add(new GenericControllerFeatureProvider());
            });

            mvcBuilder.Services.Configure<MvcOptions>(o =>
            {
                o.Conventions.Add(new GenericControllerRouteConvention());
                //o.Filters.Add(new )
            });
        }
    }
}
