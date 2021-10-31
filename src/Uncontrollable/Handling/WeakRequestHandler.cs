using System.Threading.Tasks;

namespace Uncontrollable
{
    internal class WeakRequestHandler<TRequest> : IWeakRequestHandler
    {
        private readonly IRequestHandler<TRequest> _strongHandler;

        public WeakRequestHandler(IRequestHandler<TRequest> strongHandler)
            => _strongHandler = strongHandler;

        public Task<object> Handle(object request)
            => _strongHandler.Handle((TRequest)request);
    }
}