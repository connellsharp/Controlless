using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Controlless
{
    public interface IRequestBinder
    {
        object? Bind(HttpRequest request, CancellationToken ct);
    }
}