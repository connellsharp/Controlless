using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    public interface IRequestHandler<in T>
    {
        Task Handle(T request, HttpResponse response);
    }
}