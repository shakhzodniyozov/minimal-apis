using Application.Common.Endpoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.CreateFruit;

public class CreateFruit : BaseEndpoint<CreateFruitRequest>
{
    public override void AddRoute(IEndpointRouteBuilder app)
    {
        Post(app, "/fruits/create/{id}", async ([FromBody] CreateFruitRequest request,
            [FromServices] CreateFruitHandler handler,
            CancellationToken cancellationToken = default) =>
        {
            var parameters = new RequestParameters<CreateFruitRequest>(request);

            await handler.HandleAsync(parameters, cancellationToken);
        });
    }
}