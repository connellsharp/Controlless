using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    public interface IRequestFinder
    {
        IMatchedRequest Find(HttpContext context);
    }
}