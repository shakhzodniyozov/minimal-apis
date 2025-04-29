using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using MinimalApi.Library.Endpoints;
using MinimalApi.Library.Responses;

namespace Application.Features.Fruits.GetFruitById;

public class GetFruitByIdHandler(IApplicationDbContext dbContext) : EndpointHandler<GetFruitByIdResponse>
{
    public override async Task<IResult> HandleAsync(RequestParameters? requestParameters,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(requestParameters!.RouteParameters!["id"], out var id))
            return ErrorResponse(ResponseErrorCode.BadRequest);

        var fruit = await dbContext.Fruits.FindAsync(id);
        
        if (fruit is null)
            return ErrorResponse(ResponseErrorCode.BadRequest, "Fruit not found");
        
        return SuccessResponse(new GetFruitByIdResponse() {Id = fruit.Id, Name = fruit.Name});
    }
}