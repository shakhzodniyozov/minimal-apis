using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityConfigurations;

public class FruitConfiguration : IEntityTypeConfiguration<Fruit>
{
    public void Configure(EntityTypeBuilder<Fruit> builder)
    {
        builder.ToTable("fruits");
        
        builder.HasKey(x => x.Id);
    }
}