using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;

namespace Uncontrollable
{
    /// <remarks>
    /// Inspired by https://blog.markvincze.com/matching-route-templates-manually-in-asp-net-core/
    /// </remarks>
    public static class RouteMatcherExtensions
    {
        /// <summary>
        /// Matches the path against a given route template and returns route values.
        /// </summary>
        public static bool TryMatchRoute(this HttpRequest request, string routeTemplate, out RouteValueDictionary routeValues)
        {
            var template = TemplateParser.Parse(routeTemplate);

            var matcher = new TemplateMatcher(template, GetDefaults(template));

            routeValues = new RouteValueDictionary();

            return matcher.TryMatch(request.Path, routeValues);
        }

        private static RouteValueDictionary GetDefaults(RouteTemplate parsedTemplate)
        {
            var result = new RouteValueDictionary();

            foreach (var parameter in parsedTemplate.Parameters)
            {
                if (parameter.DefaultValue != null && parameter.Name != null)
                {
                    result.Add(parameter.Name, parameter.DefaultValue);
                }
            }

            return result;
        }
    }
}