using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Controlless.Custom
{
    public interface IRequestBinder
    {
        string Method { get; }

        string Route { get; }

        object Bind(HttpRequest request, CancellationToken ct);
    }
}