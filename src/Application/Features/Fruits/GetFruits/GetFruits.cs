using Application.Common.Endpoint;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Fruits.GetFruits;

public class GetFruits : BaseEndpoint<GetFruitsResponse>
{
    public override void AddRoute(IEndpointRouteBuilder app)
    {
        Get(app, "/fruits", async ([FromServices] GetFruitsHandler handler,
            [FromQuery] int priceFrom,
            [FromQuery] int priceTo,
            CancellationToken cancellationToken) =>
        {
            handler.RequestParameters.QueryParameters[nameof(priceFrom)] = priceFrom.ToString();
            handler.RequestParameters.QueryParameters[nameof(priceTo)] = priceTo.ToString();

            return await handler.HandleAsync(cancellationToken);
        });
    }
}