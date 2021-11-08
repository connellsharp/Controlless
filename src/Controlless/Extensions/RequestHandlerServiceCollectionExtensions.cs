using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    public static class RequestHandlerServiceCollectionExtensions
    {
        public static void AddRequestHandlers(this IServiceCollection services)
            => services.AddRequestHandlers(Assembly.GetCallingAssembly());

        public static void AddRequestHandlers(this IServiceCollection services, Type type)
            => services.AddRequestHandlers(type.Assembly);

        public static void AddRequestHandlers(this IServiceCollection services, Assembly assembly)
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
    }
}
