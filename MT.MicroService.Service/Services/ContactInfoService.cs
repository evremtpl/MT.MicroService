using MT.MicroService.Core.Entity;
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
    public class ContactInfoService :Service<ContactInfo>, IContactInfoService
    {

        

        public ContactInfoService(IUnitOfWork unitOfWork, IGenericRepository<ContactInfo> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<ContactInfo> GetWithPersonByIdAsync(int id)
        {
            return await _unitOfWork.ContactInfos.GetWithPersonByIdAsync(id);
        }

       
    }
}
