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

        [Fact]
        public async Task TestResponseContainsIdFromUrl()
        {
            var response = await _client.PostAsJsonAsync("/test/4321", new { });

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("4321", responseString);
        }

        [Fact]
        public async Task TestResponseContainsIdFromBody()
        {
            var response = await _client.PostAsJsonAsync("/test/123", new
            {
                bodyString = "SomeValue"
            });

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("SomeValue", responseString);
        }

        [Fact]
        public async Task TestResponseContainsIdFromQuery()
        {
            var response = await _client.PostAsJsonAsync("/test/123?myKey=abcde", new { });

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("abcde", responseString);
        }
    }
}
