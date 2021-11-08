using System;
using System.Net;

namespace Controlless
{
    public class StatusCodeAttribute : Attribute
    {
        public StatusCodeAttribute(HttpStatusCode statusCode)
            : this((int)statusCode)
        {
        }

        public StatusCodeAttribute(int statusCode)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}