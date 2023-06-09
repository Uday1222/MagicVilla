﻿using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveAsync();

        Task<List<T>>? GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, int pageSize = 0, int pageNumber = 1);

        Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
    }
}
