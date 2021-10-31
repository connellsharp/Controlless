using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    public interface IRequestBinder
    {
        object Bind(HttpContext context);
    }
}