using System.Threading;
using System.Threading.Tasks;

namespace Controlless
{
    public interface IRequestHandler<in TRequest>
    {
        Task<object> Handle(TRequest request, CancellationToken ct);
    }
}