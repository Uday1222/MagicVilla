using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveAsync();

        Task<List<T>>? GetAll(Expression<Func<T, bool>>? filter = null);

        Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true);
    }
}
