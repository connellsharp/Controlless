using System.Threading;
using System.Threading.Tasks;

namespace Controlless
{
    internal interface IWeakRequestHandler
    {
        Task<object> Handle(object request, CancellationToken ct);
    }
}