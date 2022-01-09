using MT.MicroService.Core.Repository.Interfaces;
using MT.MicroService.Core.Services;
using MT.MicroService.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Service.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity:class, new()
    {

        public readonly IUnitOfWork _unitOfWork;

        private readonly IGenericRepository<TEntity> _repository; 
        public Service(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository )
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return entity;
        }


        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Find(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
