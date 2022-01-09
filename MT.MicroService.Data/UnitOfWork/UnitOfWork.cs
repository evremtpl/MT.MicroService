using MT.MicroService.Core.Repository.Interfaces;
using MT.MicroService.Core.UnitOfWork;
using MT.MicroService.Data;
using MT.MicroService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private PersonRepository _personRepository;

        private ContactInfoRepository _contactInfoRepository;
        public IPersonRepository Persons => _personRepository = _personRepository ?? new PersonRepository(_appDbContext);

        public IContactInfoRepository ContactInfos => _contactInfoRepository =
            _contactInfoRepository ?? new ContactInfoRepository(_appDbContext);

        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Commit()
        {
            _appDbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
