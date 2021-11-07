using Microsoft.AspNetCore.Mvc;

namespace Controlless.FunctionalTests.Sample
{
    [ControlGet("/test/{id}")]
    public class TestRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}