using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class ServiceCollectionHandlerExtensions
    {
        public static void AddControllessHandlers(this IServiceCollection services)
            => services.AddControllessHandlers(Assembly.GetCallingAssembly());

        public static void AddControllessHandlers(this IServiceCollection services, Type type)
            => services.AddControllessHandlers(type.Assembly);

        public static void AddControllessGenericRequestHandlers(this IServiceCollection services, Assembly assembly)
        {
            services.AddControllessGenericRequestHandlers(assembly);
            services.AddControllessHandlers(assembly);
        }

        public static void AddControllessHandlers(this IServiceCollection services, Assembly assembly)
        {
            var genericHandlerTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i == typeof(IRequestHandler<>)));

            foreach(var genericHandlerType in genericHandlerTypes)
            {
                services.AddSingleton(typeof(IRequestHandler<>), genericHandlerType);
            }

            var concreteHandlerTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)));

            foreach(var concreteHandlerType in concreteHandlerTypes)
            {
                var requestTypes = concreteHandlerType.GetInterfaces()
                                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
                                    .Select(t => t.GenericTypeArguments[0]);

                foreach(var requestType in requestTypes)
                {
                    services.AddSingleton(typeof(IRequestHandler<>).MakeGenericType(requestType), concreteHandlerType);
                }
            }
        }

        private static void AddControllessConcreteRequestHandlers(this IServiceCollection services, Assembly assembly)
        {
            var concreteHandlerTypes = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<>)));

            foreach(var concreteHandlerType in concreteHandlerTypes)
            {
                var requestTypes = concreteHandlerType.GetInterfaces()
                                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
                                    .Select(t => t.GenericTypeArguments[0]);

                foreach(var requestType in requestTypes)
                {
                    services.AddSingleton(typeof(IRequestHandler<>).MakeGenericType(requestType), concreteHandlerType);
                }
            }
        }
    }
}
