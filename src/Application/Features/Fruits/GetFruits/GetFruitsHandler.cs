using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.GetFruits;

public class GetFruitsHandler(IApplicationDbContext dbContext)
    : EndpointHandler<List<GetFruitsResponse>>
{
    public override async Task<IResult> HandleAsync(RequestParameters? requestParameters,
        CancellationToken cancellationToken = default)
    {
        var fruits = await dbContext.Fruits.Select(x => new GetFruitsResponse()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        return SuccessResponse(fruits);
    }
}