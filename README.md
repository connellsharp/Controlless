# Controlless

Controlless is a little experiment with ASP.NET to see if we can make APIs without Controllers. Instead we could bind routes to DTOs, handle them with something like [MediatR](https://github.com/jbogard/MediatR), then write the HTTP responses depending on the response type.

## Binding

Just put these familiar-looking attributes straight onto your request DTO.

```c#
[HttpGet("/films/{id}/actors")]
public class GetFilmActorsRequest
{
    [FromRoute("id")]
    public string FilmId { get; set; }

    [FromQuery("page")]
    public int Page { get; set; }
}
```

## Handling

Then you register an `IRequestHandler<T>` that does the work and returns a response object.

```c#
public class GetFilmActorsRequestHandler : IRequestHandler<GetFilmActorsRequest>
{
    public Task<object> Handle(GetFilmActorsRequest request, CancellationToken ct)
    {
        // handle the request
        return responseObject;
    }
}
```

Or maybe you just register a generic handler that forwards everything to MediatR.

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

Then finally, an `IResponseWriter<T>` writes the response object back to the HTTP stream. For example, you could create one for validation failures.

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