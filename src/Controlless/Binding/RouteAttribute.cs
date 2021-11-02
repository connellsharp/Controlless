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

    public class HttpGetAttribute : RouteAttribute
    {
        public HttpGetAttribute(string routePattern)
            : base("GET", routePattern) { }
    }

    public class HttpPostAttribute : RouteAttribute
    {
        public HttpPostAttribute(string routePattern)
            : base("POST", routePattern) { }
    }

    public class HttpPutAttribute : RouteAttribute
    {
        public HttpPutAttribute(string routePattern)
            : base("PUT", routePattern) { }
    }

    public class HttpPatchAttribute : RouteAttribute
    {
        public HttpPatchAttribute(string routePattern)
            : base("PATCH", routePattern) { }
    }

    public class HttpDeleteAttribute : RouteAttribute
    {
        public HttpDeleteAttribute(string routePattern)
            : base("DELETE", routePattern) { }
    }
}