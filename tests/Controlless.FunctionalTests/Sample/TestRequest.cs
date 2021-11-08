using Microsoft.AspNetCore.Mvc;

namespace Controlless.FunctionalTests.Sample
{
    [ControlPost("/test/{id}")]
    public class TestRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}