using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.GetFruits;

public class GetFruitsHandler(IApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    : EndpointHandler<List<GetFruitsResponse>>(httpContextAccessor)
{
    public override async Task<IResult> HandleAsync(RequestParameters? requestParameters,
        CancellationToken cancellationToken = default)
    {
        var id = FromRoute<int>("id");

        var fruits = await dbContext.Fruits.Select(x => new GetFruitsResponse()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);

        return SuccessResponse(fruits);
    }
}