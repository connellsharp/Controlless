using System.Threading;
using System.Threading.Tasks;

namespace Controlless.FunctionalTests.Sample
{
    internal class TestRequestHandler : IRequestHandler<TestRequest>
    {
        public async Task<object> Handle(TestRequest request, CancellationToken cancellationToken)
        {
            return new
            {
                requestId = request.Id
            };
        }
    }
}