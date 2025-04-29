using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using MinimalApi.Library.Endpoints;
using MinimalApi.Library.Responses;

namespace Application.Features.Fruits.GetFruitById;

public class GetFruitByIdHandler(IApplicationDbContext dbContext)
    : EndpointHandler<GetFruitsByIdRequest, GetFruitByIdResponse>
{
    public override async Task<IResult> HandleAsync(GetFruitsByIdRequest request,
        CancellationToken cancellationToken)
    {
        var fruit = await dbContext.Fruits.FindAsync(request.Id);

        if (fruit is null)
            return ErrorResponse(ResponseErrorCode.BadRequest, "Fruit not found");

        return SuccessResponse(new GetFruitByIdResponse { Id = fruit.Id, Name = fruit.Name });
    }
}