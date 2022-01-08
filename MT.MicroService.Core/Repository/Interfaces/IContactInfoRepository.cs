using MT.MicroService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Repository.Interfaces
{
  public  interface IContactInfoRepository: IGenericRepository <ContactInfo>
    {
        Task<ContactInfo> GetWithPersonByIdAsync(int id); //ister dışında extra method
    }
}
