namespace Application.Features.Fruits.GetFruits;

public class GetFruitsResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}