using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Controlless
{
    internal interface IWeakResponseWriter
    {
        Task Write(object responseObject, HttpResponse httpResponse, CancellationToken ct);
    }
}