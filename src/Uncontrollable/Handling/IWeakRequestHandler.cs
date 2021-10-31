using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    internal interface IWeakRequestHandler
    {
        Task Handle(object request, HttpResponse response);
    }
}