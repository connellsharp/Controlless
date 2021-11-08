using System.Threading;
using System.Threading.Tasks;

namespace Controlless.FunctionalTests.Sample
{
    public class TestRequestHandler : IRequestHandler<TestRequest>
    {
        public async Task<object> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            return new
            {
                requestId = request.Id,
                requestBodyString = request.Body.BodyString,
                requestQueryKey = request.MyKey
            };
        }
    }
}