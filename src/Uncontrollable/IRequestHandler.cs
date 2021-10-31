using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    internal interface IWeakRequestHandler
    {
        Task Handle(object request, HttpResponse response);
    }

    public interface IRequestHandler<in T>
    {
        Task Handle(T request, HttpResponse response);
    }

    internal class WeakRequestHandler<T> : IWeakRequestHandler
    {
        private readonly IRequestHandler<T> _strongHandler;

        public WeakRequestHandler(IRequestHandler<T> strongHandler)
            => _strongHandler = strongHandler;

        public Task Handle(object request, HttpResponse response)
            => _strongHandler.Handle((T)request, response);
    }
}