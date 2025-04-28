using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Fruits.CreateFruit;

public record CreateFruitRequest
{
    [FromRoute, JsonIgnore]
    public Guid Id { get; set; }
    public required string Name { get; init; }
    public decimal Price { get; set; }
}