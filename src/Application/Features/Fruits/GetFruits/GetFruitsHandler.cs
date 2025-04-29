using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.GetFruits;

public class GetFruitsHandler(IApplicationDbContext dbContext)
    : EndpointHandler<GetFruitsRequest, List<GetFruitsResponse>>
{
    public override async Task<IResult> HandleAsync(GetFruitsRequest request,
        CancellationToken cancellationToken)
    {
        var fruits = await dbContext.Fruits.AsNoTracking()
            .Where(x => x.Price >= request.PriceFrom && x.Price <= request.PriceTo)
            .Select(x => new GetFruitsResponse
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            })
            .ToListAsync(cancellationToken);

        return SuccessResponse(fruits);
    }
}