using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    internal interface IWeakResponseWriter
    {
        Task Write(object responseObject, HttpResponse httpResponse);
    }
}