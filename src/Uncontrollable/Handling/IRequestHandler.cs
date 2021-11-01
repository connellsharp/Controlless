using System.Threading;
using System.Threading.Tasks;

namespace Uncontrollable
{
    public interface IRequestHandler<in TRequest>
    {
        Task<object> Handle(TRequest request, CancellationToken ct);
    }
}