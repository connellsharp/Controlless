using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Controlless.Binding
{
    internal class ControllerlessBaseController<T> : Controller
    {
        private readonly IRequestHandler<T> _requestHandler;

        public ControllerlessBaseController(IRequestHandler<T> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        public Task<object> Handle(T request, CancellationToken ct)
        {
            return _requestHandler.Handle(request, ct);
        }
    }
}