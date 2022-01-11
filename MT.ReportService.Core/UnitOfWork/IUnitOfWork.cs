
using System.Threading.Tasks;

namespace MT.ReportService.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
   
        Task CommitAsync();
        void Commit();
    }
}
