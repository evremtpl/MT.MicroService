using MT.MicroService.Core.Entity;
using MT.MicroService.Core.Repository.Interfaces;
using MT.MicroService.Core.Services;
using MT.MicroService.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Service.Services
{
    public class PersonService : Service<Person>, IPersonService
    {
        public PersonService(IUnitOfWork unitOfWork, IGenericRepository<Person> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Person> GetWithContactInfoByPersonIdAsync(int personId)
        {
           return await _unitOfWork.Persons.GetWithContactInfoByPersonIdAsync(personId);
        }
    }
}
