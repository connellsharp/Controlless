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

    public class ControlGetAttribute : RouteAttribute
    {
        public ControlGetAttribute(string routePattern)
            : base("GET", routePattern) { }
    }

    public class ControlPostAttribute : RouteAttribute
    {
        public ControlPostAttribute(string routePattern)
            : base("POST", routePattern) { }
    }

    public class ControlPutAttribute : RouteAttribute
    {
        public ControlPutAttribute(string routePattern)
            : base("PUT", routePattern) { }
    }

    public class ControlPatchAttribute : RouteAttribute
    {
        public ControlPatchAttribute(string routePattern)
            : base("PATCH", routePattern) { }
    }

    public class ControlDeleteAttribute : RouteAttribute
    {
        public ControlDeleteAttribute(string routePattern)
            : base("DELETE", routePattern) { }
    }
}