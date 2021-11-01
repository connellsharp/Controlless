using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Controlless
{
    internal class RequestProcessor
    {
        private HttpContext _context;

        public RequestProcessor(HttpContext context)
        {
            _context = context;
        }

        public async Task ProcessRequest(object request)
        {   
            var handler = GetWeakServiceAdapter<IWeakRequestHandler>(typeof(RequestHandlerWeakAdapter<>), request.GetType());
            var response = await handler.Handle(request, _context.RequestAborted);

            var writer = GetWeakServiceAdapter<IWeakResponseWriter>(typeof(ResponseWriterWeakAdapter<>), response.GetType());
            await writer.Write(response, _context.Response, _context.RequestAborted);
        }

        private T GetWeakServiceAdapter<T>(Type adapterType, Type genericParameter)
        {
            var serviceType = adapterType.MakeGenericType(genericParameter);
            return (T)_context.RequestServices.GetRequiredService(serviceType);
        }
    }
}
