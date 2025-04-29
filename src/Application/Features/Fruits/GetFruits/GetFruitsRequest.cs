using System.Text.Json.Serialization;

namespace Application.Features.Fruits.GetFruits;

public class GetFruitsRequest
{
    [JsonIgnore]
    public int PriceFrom { get; set; }
    
    [JsonIgnore]
    public int PriceTo { get; set; }
}