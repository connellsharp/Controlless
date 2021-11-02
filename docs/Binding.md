
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
            FilmId = routeValues["id"],
            // set other props from query or body
        };
    }
}
```

Or instead, maybe you could hook up a generic binder that uses attributes to bind straight onto the DTOs.