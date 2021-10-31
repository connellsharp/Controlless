using Microsoft.AspNetCore.Http;

namespace Uncontrollable
{
    internal interface IRequestFinder
    {
        object Find(HttpContext context);
    }
}