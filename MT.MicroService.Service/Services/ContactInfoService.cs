using MT.MicroService.Core.Entity;
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
    public class ContactInfoService : IContactInfoService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ContactInfoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ContactInfo> AddAsync(ContactInfo entity)
        {
            await _unitOfWork.ContactInfos.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return entity;
        }

        public void Delete(ContactInfo entity)
        {
            _unitOfWork.ContactInfos.Delete(entity);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<ContactInfo>> Find(Expression<Func<ContactInfo, bool>> predicate)
        {
           return await _unitOfWork.ContactInfos.Find(predicate);
        }

        public async Task<IEnumerable<ContactInfo>> GetAllAsync()
        {
            return await _unitOfWork.ContactInfos.GetAllAsync();
        }

        public async Task<ContactInfo> GetByIdAsync(int id)
        {
            return await _unitOfWork.ContactInfos.GetByIdAsync(id);
        }

        public async Task<ContactInfo> GetWithPersonByIdAsync(int id)
        {
            return await _unitOfWork.ContactInfos.GetWithPersonByIdAsync(id);
        }

       
    }
}
