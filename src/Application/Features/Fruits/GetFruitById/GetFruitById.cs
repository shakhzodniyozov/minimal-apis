using Application.Common.Endpoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.GetFruitById;

public class GetFruitById : BaseEndpoint<GetFruitByIdResponse>
{
    public override void AddRoute(IEndpointRouteBuilder app)
    {
        Get(app, "/fruits/{id}", async (GetFruitByIdHandler handler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken) =>
        {
            handler.RequestParameters.RouteParameters["id"] = id.ToString();

            return await handler.HandleAsync(cancellationToken);
        });
    }
}