using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Controlless.FunctionalTests
{
    public class BasicTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public BasicTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData(123)]
        [InlineData(4321)]
        public async Task TestResponseContainsIdFromUrl(int testId)
        {
            var response = await _client.PostAsJsonAsync("/test/" + testId, new {
                bodyString = "SomeValue"
            });

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains(testId.ToString(), responseString);
        }
    }
}
