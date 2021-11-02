using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class FinderHandlerServiceCollectionExtensions
    {
        public static void AddControllessCore(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton(typeof(RequestHandlerWeakAdapter<>));
            services.AddSingleton(typeof(ResponseWriterWeakAdapter<>));
            services.AddSingleton(typeof(IResponseWriter<>), typeof(JsonResponseWriter<>));
        }

        public static void AddRequestBinders(this IServiceCollection services, Type type)
            => services.AddRequestBinders(type.Assembly);

        public static void AddRequestBinders(this IServiceCollection services, Assembly assembly)
        {
            services.AddControllessCore();

            var binderTypes = assembly.GetExportedTypes()
                .Where(t => typeof(IRequestBinder).IsAssignableFrom(t));

            foreach(var binderType in binderTypes)
            {
                services.AddSingleton(typeof(IRequestBinder), binderType);
            }
        }

        public static void AddRequestModelBinders(this IServiceCollection services, Type type)
            => services.AddRequestModelBinders(type.Assembly);

        public static void AddRequestModelBinders(this IServiceCollection services, Assembly assembly)
        {
            services.AddControllessCore();

            var modelTypes = assembly.GetExportedTypes()
                .Where(t => t.GetCustomAttribute<RouteAttribute>() != null);

            foreach(var modelType in modelTypes)
            {
                services.AddSingleton(typeof(IRequestBinder), typeof(ModelRequestBinder<>).MakeGenericType(modelType));
            }
        }
    }
}
