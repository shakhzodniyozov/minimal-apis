using System.Text.Json.Serialization;

namespace Application.Features.Fruits.GetFruitById;

public class GetFruitsByIdRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
}