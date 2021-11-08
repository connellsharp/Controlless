# Controlless

Controlless is a little experiment with ASP.NET to see if we can make APIs without Controllers. Instead we could bind routes to DTOs, handle them with something like [MediatR](https://github.com/jbogard/MediatR), then write the HTTP responses depending on the response type.

## Binding

Just put these `[RouteGet]` and `[RoutePost]` attributes and your action filters straight onto your request DTOs.

```c#
[RoutePost("/films/{id}")]
[Authorize]
public class CreateFilmRequest
{
    [FromRoute("id")]
    public string FilmId { get; set; }

    [FromBody]
    public Body Body { get; set; }

    public class Body
    {
        public string Name { get; set; }
    }
}

[RouteGet("/films/{id}/actors")]
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

You can use the `[StatusCode]` attribute to respond with a different status code when a certain response type is returned.

```c#
[StatusCode(201)]
public class CreateFilmResponse
{
    public string FilmId { get; set; }
}
```

Or if you don't have control over the type, returning from a handler is exactly the same as returning from a controller. You could return an `IActionFilter` for example. Or you could register a global response filter to change the status code when a certain type is returned.

```c#
public class ValidationFailureResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if(context.Result is ObjectResult objectResult
            && objectResult.Value is List<ValidationFailure>)
        {
            context.HttpContext.Response.StatusCode = 400;
        }
    }
}
```

# Related articles

- [Look, no controllers](https://www.connell.dev/look-no-controllers)