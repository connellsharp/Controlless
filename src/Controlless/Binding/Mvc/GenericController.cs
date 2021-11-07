using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Controlless.Binding
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var assemblyTypes = parts.OfType<AssemblyPart>().SelectMany(ap => ap.Types);
            var candidates = assemblyTypes
                //.Where(type => !typeof(Controller).IsAssignableFrom(type))
                .Where(type => type.GetCustomAttributes<RouteAttribute>().Any());
                
            foreach (var candidate in candidates)
            {
                feature.Controllers.Add(
                    typeof(BaseController<>).MakeGenericType(candidate).GetTypeInfo()
                );
            }
        }
    }

    internal class BaseController<T> : Controller
    {
        private readonly IRequestHandler<T> _requestHandler;

        public BaseController(IRequestHandler<T> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        [HttpGet]
        public Task<object> Handle(T request, CancellationToken ct)
        {
            return _requestHandler.Handle(request, ct);
        }
    }

    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType
                && controller.ControllerType.GetGenericTypeDefinition() == typeof(BaseController<>))
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var httpMethodAttribute = genericType.GetCustomAttribute<RouteAttribute>();
    
                if (httpMethodAttribute != null)
                {
                    controller.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(httpMethodAttribute.RoutePattern))
                    });
                }
            }
        }
    }
}