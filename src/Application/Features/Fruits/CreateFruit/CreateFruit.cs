using Application.Common.Endpoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Fruits.CreateFruit;

public class CreateFruit : BaseEndpoint<CreateFruitRequest>
{
    public override void AddRoute(IEndpointRouteBuilder app)
    {
        Post(app, "/fruits/create", SetHandler);
    }

    private static async Task<IResult> SetHandler([FromServices] CreateFruitHandler handler,
        [FromBody] CreateFruitRequest request,
        CancellationToken cancellationToken = default)
    {
        return await handler.HandleAsync(request, cancellationToken);
    }
}