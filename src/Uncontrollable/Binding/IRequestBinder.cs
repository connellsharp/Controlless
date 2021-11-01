using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    public interface IRequestBinder
    {
        object Bind(HttpRequest request, CancellationToken ct);
    }
}