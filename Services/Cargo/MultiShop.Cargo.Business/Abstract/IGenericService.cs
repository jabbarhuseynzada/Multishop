namespace MultiShop.Cargo.Business.Abstract;
public interface IGenericService<T> where T : class
{
    Task<T> GetTByIdAsync(int id);
    Task<IEnumerable<T>> GetAllTAsync();
    Task AddTAsync(T entity);
    Task UpdateTAsync(T entity);
    Task DeleteTAsync(int id);
    Task<bool> ExistsTAsync(int id);
}
