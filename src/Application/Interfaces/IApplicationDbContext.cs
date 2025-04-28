using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Fruit> Fruits { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}