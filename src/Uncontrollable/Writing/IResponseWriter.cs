using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    public interface IResponseWriter<in T>
    {
        Task Write(T responseObject, HttpResponse httpResponse);
    }
}