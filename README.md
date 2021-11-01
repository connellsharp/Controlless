# Controlless

Controlless is a little experiment with ASP.NET to see if we can make APIs without Controllers. Instead we could bind routes to DTOs, handle them with something like [MediatR](https://github.com/jbogard/MediatR), then write the HTTP responses depending on the response type.

## Binding

Register as many `IRequestBinder` implementations as you need. These will bind the `HttpRequest` to a request DTO.

```c#
internal class WatchFilmRequestBinder : IRequestBinder
{
    public object? Bind(HttpRequest request, CancellationToken ct)
    {
        if(request.Method != "POST" || !request.TryMatchRoute("/films/{id}/watch", out var routeValues))
            return null;
            
        return new WatchFilmRequest
        {
            FilmId = id,
            // set other props from query or body
        };
    }
}
```

Maybe you could hook up a generic binder that uses attributes to bind straight onto the DTO.

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

## Handling

Then you register a handler that does the work and returns a response object.

```c#
public class WatchFilmRequestHandler : IRequestHandler<WatchFilmRequest>
{
    public Task<object> Handle(WatchFilmRequest request, CancellationToken ct)
    {
        // handle the request
        return responseObject;
    }
}
```

Or maybe you just hook up a generic handler that forwards everything to MediatR.

```c#
public class MediatorRequestHandler<TRequest> : IRequestHandler<TRequest>
{
    private readonly IMediator _mediator;

    public MediatorRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<object> Handle(TRequest request, CancellationToken ct)
    {
        return await _mediator.Send(request, ct);
    }
}
```

## Responding

Then finally, an `IResponseWriter<T>` writes that object back to the HTTP stream. You could set one 

```c#
internal class ValidationFailureJsonResponseWriter : IResponseWriter<ValidationFailure>
{
    public async Task Write(ValidationFailure responseObject, HttpResponse response, CancellationToken ct)
    {
        response.StatusCode = 400;
        await response.WriteAsync("Validation error", ct);
    }
}
```

No worries if you don't register one for every response type. It'll fall back to the generic registration that writes a JSON response.

```c#
internal class JsonResponseWriter<T> : IResponseWriter<T>
{
    public async Task Write(T responseObject, HttpResponse response, CancellationToken ct)
    {
        await response.WriteAsJsonAsync(responseObject, ct);
    }
}
```