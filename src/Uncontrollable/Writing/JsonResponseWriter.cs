using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    internal class JsonResponseWriter<T> : IResponseWriter<T>
    {
        public async Task Write(T responseObject, HttpResponse response)
        {
            await response.WriteAsJsonAsync(responseObject);
        }
    }
}