using Application.Common.Endpoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Fruits.GetFruitById;

public class GetFruitById : BaseEndpoint<GetFruitByIdRequest, GetFruitByIdResponse>
{
    public override void AddRoute(IEndpointRouteBuilder app)
    {
        Get(app, "/fruits/{id}", SetHandler);
    }

    private async Task<IResult> SetHandler([FromRoute] Guid id, [FromServices] GetFruitByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        return await handler.HandleAsync(new() { Id = id }, cancellationToken);
    }
}