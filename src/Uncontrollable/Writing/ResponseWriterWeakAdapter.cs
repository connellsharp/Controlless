using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    internal class ResponseWriterWeakAdapter<T> : IWeakResponseWriter
    {
        private readonly IResponseWriter<T> _responseWriter;

        public ResponseWriterWeakAdapter(IResponseWriter<T> responseWriter)
        {
            _responseWriter = responseWriter;
        }

        public Task Write(object responseObject, HttpResponse httpResponse, CancellationToken ct)
        {
            return _responseWriter.Write((T)responseObject, httpResponse, ct);
        }
    }
}