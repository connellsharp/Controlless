# Uncontrollable

Uncontrollable is a little experiment with ASP.NET Endpoints to see if we can automatically bind routes to DTOs and handle them with something like (MediatR)[https://github.com/jbogard/MediatR] instead of having to write Controllers and Actions.

Imagine something like
```c#
[HttpPut("/things/{id}/details")]
public class UpdateThingDetailsRequest
{
    [FromRoute("id")]
    public string ThingId { get; set; }

    [FromBody]
    public string Name { get; set; }

    [FromBody]
    public string Type { get; set; }
}
```