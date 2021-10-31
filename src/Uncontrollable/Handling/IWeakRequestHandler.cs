using System.Threading.Tasks;

namespace Uncontrollable
{
    internal interface IWeakRequestHandler
    {
        Task<object> Handle(object request);
    }
}