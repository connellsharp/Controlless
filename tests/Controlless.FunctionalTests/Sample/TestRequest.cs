namespace Controlless.FunctionalTests.Sample
{
    [HttpGet("/test/{id}")]
    public class TestRequest
    {
        [FromRoute("id")]
        public int Id { get; set; }
    }
}