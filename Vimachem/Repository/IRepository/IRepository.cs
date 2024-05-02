using System.Linq.Expressions;
using Vimachem.Models.Domain;

namespace Vimachem.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool track = true);
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool track = true, params string[] includePaths);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params string[] includePaths);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
