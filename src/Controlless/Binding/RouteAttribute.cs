using System;

namespace Controlless
{
    public class RouteAttribute : Attribute
    {
        public RouteAttribute(string method, string routePattern)
        {
            Method = method;
            RoutePattern = routePattern;
        }

        public string Method { get; }

        public string RoutePattern { get; }
    }

    public class RouteGetAttribute : RouteAttribute
    {
        public RouteGetAttribute(string routePattern)
            : base("GET", routePattern) { }
    }

    public class RoutePostAttribute : RouteAttribute
    {
        public RoutePostAttribute(string routePattern)
            : base("POST", routePattern) { }
    }

    public class RoutePutAttribute : RouteAttribute
    {
        public RoutePutAttribute(string routePattern)
            : base("PUT", routePattern) { }
    }

    public class RoutePatchAttribute : RouteAttribute
    {
        public RoutePatchAttribute(string routePattern)
            : base("PATCH", routePattern) { }
    }

    public class RouteDeleteAttribute : RouteAttribute
    {
        public RouteDeleteAttribute(string routePattern)
            : base("DELETE", routePattern) { }
    }
}