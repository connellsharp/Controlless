using System.Threading;
using System.Threading.Tasks;

namespace Uncontrollable
{
    internal class RequestHandlerWeakAdapter<TRequest> : IWeakRequestHandler
    {
        private readonly IRequestHandler<TRequest> _strongHandler;

        public RequestHandlerWeakAdapter(IRequestHandler<TRequest> strongHandler)
            => _strongHandler = strongHandler;

        public Task<object> Handle(object request, CancellationToken ct)
            => _strongHandler.Handle((TRequest)request, ct);
    }
}