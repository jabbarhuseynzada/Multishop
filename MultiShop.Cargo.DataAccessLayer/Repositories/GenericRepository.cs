using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.Repositories;
public class GenericRepository<T>(CargoContext context) : IGenericDal<T> where T : class
{
    private readonly CargoContext _context = context;

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }

    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id) != null;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var values = await _context.Set<T>().ToListAsync();
        if (values == null || !values.Any())
        {
            throw new KeyNotFoundException("No entities found.");
        }
        return values;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var value = await _context.Set<T>().FindAsync(id);
        if (value == null)
        {
            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }
        return value;
    }

    public async Task UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
        }
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
}
