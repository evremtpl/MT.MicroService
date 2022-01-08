using MT.MicroService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Repository.Interfaces
{
   public  interface IPersonRepository :IGenericRepository <Person>
    {
        Task<Person> GetWithContactInfoByPersonIdAsync(int personId);
    }
}
