using MT.MicroService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Repository.Interfaces
{
    interface IPersonRepository :IGenericRepository <Person>
    {
        Task<Person> GetWithContactInfoByPersonIdAsync(int personId);
    }
}
