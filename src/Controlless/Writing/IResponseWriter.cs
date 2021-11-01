using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Controlless
{
    public interface IResponseWriter<in T>
    {
        Task Write(T responseObject, HttpResponse httpResponse, CancellationToken ct);
    }
}