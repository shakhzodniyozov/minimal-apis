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
        Post(app, "/fruits/create", async ([FromBody] CreateFruitRequest request,
            [FromServices] CreateFruitHandler handler,
            CancellationToken cancellationToken = default) =>
        {
            handler.RequestParameters.Request = request;
            
            return await handler.HandleAsync(cancellationToken);
        });
    }
}