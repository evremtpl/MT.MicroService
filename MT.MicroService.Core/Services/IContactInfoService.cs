using MT.MicroService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.Services
{
    public interface IContactInfoService : IServices <ContactInfo>
    {
        Task<ContactInfo> GetWithPersonByIdAsync(int id); //ister dışında extra method
    }
}
