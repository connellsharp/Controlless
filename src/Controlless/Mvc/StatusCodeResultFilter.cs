using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controlless.Binding
{
    public class StatusCodeResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.Result is ObjectResult objectResult
                && objectResult.Value is not null)
            {
                var statusCodeAttribute = objectResult.Value.GetType().GetCustomAttribute<StatusCodeAttribute>();
                
                if(statusCodeAttribute != null)
                {
                    context.HttpContext.Response.StatusCode = statusCodeAttribute.StatusCode;
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}