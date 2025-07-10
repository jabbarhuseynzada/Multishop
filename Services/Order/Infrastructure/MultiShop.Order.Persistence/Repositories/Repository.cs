using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Persistence.Contexts;
using System.Linq.Expressions;

namespace MultiShop.Order.Persistence.Repositories;
public class Repository<T>(OrderContext context) : IRepository<T> where T : class
{
    private readonly OrderContext _context = context;
    public async Task CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with ID {id} not found.");
        }
        return entity;
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(filter);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} matching the filter not found.");
        }
        return entity;
    }
}
