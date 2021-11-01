using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Controlless
{
    internal class JsonResponseWriter<T> : IResponseWriter<T>
    {
        public async Task Write(T responseObject, HttpResponse response, CancellationToken ct)
        {
            await response.WriteAsJsonAsync(responseObject, ct);
        }
    }
}