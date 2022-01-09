using Microsoft.EntityFrameworkCore;
using MT.MicroService.Core.Entity;
using MT.MicroService.Core.Repository.Interfaces;
using MT.MicroService.Data.FileContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Data.Repositories
{
    public class ContactInfoRepository : GenericRepository<ContactInfo>, IContactInfoRepository
    {

        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public ContactInfoRepository(DbContext context) : base(context)
        {
        }

        public async Task<ContactInfo> GetWithPersonByIdAsync(int id)
        {
            return await  _appDbContext.ContactInfos.Include(x => x.Person).SingleOrDefaultAsync(x => x.id == id);
        }
    }
}
