using Application.Common.Endpoint;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Fruits.GetFruits;

public class GetFruits : BaseEndpoint<GetFruitsResponse>
{
    public override void AddRoute(IEndpointRouteBuilder app)
    {
        Get(app, "/fruits", SetHandler);
    }
    
    private async Task<IResult> SetHandler([FromQuery] int priceFrom,
        [FromQuery] int priceTo,
        [FromServices] GetFruitsHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new GetFruitsRequest
        {
            PriceFrom = priceFrom,
            PriceTo = priceTo
        };
        
        return await handler.HandleAsync(request, cancellationToken);
    }
}