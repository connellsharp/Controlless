using Microsoft.AspNetCore.Mvc;

namespace Controlless.FunctionalTests.Sample
{
    [ControlPost("/test/{id}")]
    public class TestRequest
    {
        [FromRoute]
        public int Id { get; set; }

        [FromQuery]
        public string MyKey { get; set; }

        [FromBody]
        public TestRequestBody Body { get; set; }

        public class TestRequestBody
        {
            public string BodyString { get; set; }
        }
    }
}