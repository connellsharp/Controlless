using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controlless.Binding
{
    internal class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType
                && controller.ControllerType.GetGenericTypeDefinition() == typeof(ControllerlessBaseController<>))
            {
                AddRoutingAttributes(controller);
                AddFilterAttributes(controller);
            }
        }

        private static void AddRoutingAttributes(ControllerModel controller)
        {
            var genericType = controller.ControllerType.GenericTypeArguments[0];
            var routeAttribute = genericType.GetCustomAttribute<RouteAttribute>();

            if (routeAttribute != null)
            {
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(routeAttribute.RoutePattern)),
                    ActionConstraints =
                    {
                        new HttpMethodActionConstraint(new[] { routeAttribute.Method })
                    }
                });
            }
        }

        private static void AddFilterAttributes(ControllerModel controller)
        {
            var genericType = controller.ControllerType.GenericTypeArguments[0];
            var filterAttributes = genericType.GetCustomAttributes().OfType<IFilterMetadata>();

            foreach(var filterAttribute in filterAttributes)
            {
                controller.Filters.Add(filterAttribute);
            }
        }
    }
}