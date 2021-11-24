using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Controlless.Binding
{
    public class ResponseHeaderResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.Result is ObjectResult objectResult
                && objectResult.Value is not null)
            {
                var headerAttributes = objectResult.Value.GetType().GetCustomAttributes<ResponseHeaderAttribute>();
                
                foreach(var headerAttribute in headerAttributes)
                {
                    context.HttpContext.Response.Headers.Add(headerAttribute.Key, headerAttribute.Value);
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}