using Application.Common.Endpoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Fruits.GetFruits;

public class GetFruits : BaseEndpoint<GetFruitsResponse>
{
    public override void AddRoute(IEndpointRouteBuilder app)
    {
        Get(app, "/fruits/{id}", async ([FromServices] GetFruitsHandler handler) =>
        await handler.HandleAsync(null));
    }
}