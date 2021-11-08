using System.Reflection;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Controlless.Binding
{
    internal class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType
                && controller.ControllerType.GetGenericTypeDefinition() == typeof(ControllerlessBaseController<>))
            {
                var genericType = controller.ControllerType.GenericTypeArguments[0];
                var routeAttribute = genericType.GetCustomAttribute<RouteAttribute>();
    
                if (routeAttribute != null)
                {
                    controller.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(routeAttribute.RoutePattern)),
                        ActionConstraints = {
                            new HttpMethodActionConstraint(new[] { routeAttribute.Method })
                        }
                    });
                }
            }
        }
    }
}