using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.GetFruits;

public class GetFruitsHandler(IApplicationDbContext dbContext)
    : EndpointHandler<List<GetFruitsResponse>>
{
    public override async Task<IResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        var priceFrom = FromQuery<int>("priceFrom");
        var priceTo = FromQuery<int>("priceTo");

        var fruits = await dbContext.Fruits.AsNoTracking()
            .Where(x => x.Price >= priceFrom && x.Price <= priceTo)
            .Select(x => new GetFruitsResponse
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        return SuccessResponse(fruits);
    }
}