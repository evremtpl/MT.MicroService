
using MT.ReportService.Core.UnitOfWork;
using System.Threading.Tasks;

namespace MT.ReportService.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
     

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
