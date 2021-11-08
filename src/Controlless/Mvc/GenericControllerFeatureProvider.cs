using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Controlless.Binding
{
    internal class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var assemblyTypes = parts.OfType<AssemblyPart>().SelectMany(ap => ap.Types);

            var requestObjects = assemblyTypes.Where(type => type.GetCustomAttributes<RouteAttribute>().Any());
                
            foreach (var requestObject in requestObjects)
            {
                feature.Controllers.Add(
                    typeof(ControllerlessBaseController<>).MakeGenericType(requestObject).GetTypeInfo()
                );
            }
        }
    }
}