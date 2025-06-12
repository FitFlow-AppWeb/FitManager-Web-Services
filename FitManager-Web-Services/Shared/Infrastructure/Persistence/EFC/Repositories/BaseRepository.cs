using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitManager_Web_Services.Shared.Domain.Repositories;
using FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace FitManager_Web_Services.Shared.Infrastructure.Persistence.EFC.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entities;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entityToDelete = await _entities.FindAsync(id);
        if (entityToDelete != null)
        {
            _entities.Remove(entityToDelete);
        }
    }
}