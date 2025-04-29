using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using MinimalApi.Library.Endpoints;

namespace Application.Features.Fruits.CreateFruit;

public class CreateFruitHandler(IApplicationDbContext dbContext)
    : EndpointHandler<CreateFruitRequest, CreateFruitResponse>
{
    public override async Task<IResult> HandleAsync(CreateFruitRequest request, CancellationToken cancellationToken)
    {
        var fruit = new Fruit { Name = request.Name, Price = request.Price };

        dbContext.Fruits.Add(fruit);
        await dbContext.SaveChangesAsync(cancellationToken);

        return SuccessResponse(null);
    }
}