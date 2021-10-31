# Uncontrollable

Uncontrollable is a little experiment with ASP.NET to see if we can automatically bind routes to DTOs and handle them with something like [MediatR](https://github.com/jbogard/MediatR) instead of writing Controllers.

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

Then all you do is write the handler
```c#
[HttpPut("/things/{id}/details")]
public class UpdateThingDetailsHandler : IRequestHandler<UpdateThingDetailsRequest>
{
    public Task<object> Handle(UpdateThingDetailsRequest request)
    {
        // handle the request
        return responseObject;
    }
}
```

Or if the request was already a MedaitR request, perhaps use a generic handler. We could even validate it first with [FluentValidation](https://fluentvalidation.net/).
```c#
[HttpPut("/things/{id}/details")]
public class AllTheStuffHandler<TRequest> : IRequestHandler<TRequest>
{
    public AllTheStuffHandler(IValidator<TRequest> validator, IMediator mediator)
    {
        // set fields
    }

    public async Task<object> Handle(TRequest request)
    {
        var validationResult = _validator.Validate(request);
        
        // handle validation

        return await _mediator.Send(request);
    }
}
```