using MT.MicroService.Core.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPersonRepository Persons { get; } //DI geçmek yerine burada kullanıldı, best practise açısından.
        IContactInfoRepository ContactInfos { get; }
        Task CommitAsync();
        void Commit();
    }
}
