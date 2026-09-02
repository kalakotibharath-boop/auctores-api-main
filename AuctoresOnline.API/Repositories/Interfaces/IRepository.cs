namespace AuctoresOnline.API.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    IQueryable<T> Datatable { get; }
    Task<T?> FindAsync(params object[] keyValues);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<int> SaveChangesAsync();
}
