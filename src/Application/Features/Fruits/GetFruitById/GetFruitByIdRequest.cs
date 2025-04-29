using System.Text.Json.Serialization;

namespace Application.Features.Fruits.GetFruitById;

public class GetFruitByIdRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
}