using Microsoft.EntityFrameworkCore;
using MT.MicroService.Core.Entity;
using MT.MicroService.Core.Repository.Interfaces;
using MT.MicroService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }
        public PersonRepository(AppDbContext context) : base(context)
        {
        }
        public  async Task<Person> GetWithContactInfoByPersonIdAsync(int personId)
        {
            var a =await _appDbContext.Persons.Include(x => x.ContactInfos).SingleOrDefaultAsync(x => x.UUID == personId);
            return a;
            }
    }
}
