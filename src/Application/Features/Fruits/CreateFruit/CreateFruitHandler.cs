using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.CreateFruit;

public class CreateFruitHandler(IApplicationDbContext dbContext)
    : EndpointHandler<CreateFruitRequest, CreateFruitResponse>
{
    public override async Task<IResult> HandleAsync(RequestParameters<CreateFruitRequest> requestParameters,
        CancellationToken cancellationToken)
    {
        var fruit = new Fruit { Name = requestParameters.Request!.Name, Price = requestParameters.Request!.Price };

        dbContext.Fruits.Add(fruit);
        await dbContext.SaveChangesAsync(cancellationToken);

        return SuccessResponse(null);
    }
}