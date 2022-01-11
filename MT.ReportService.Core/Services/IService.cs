using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MT.ReportService.Core.Services
{
    public interface IService<TEntity> where TEntity :class,new()
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task <TEntity> AddAsync(TEntity entity);
        void Delete(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
