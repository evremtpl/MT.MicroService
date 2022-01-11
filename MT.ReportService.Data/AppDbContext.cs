using Microsoft.EntityFrameworkCore;
using MT.ReportService.Core.Entity;


namespace MT.ReportService.Data
{
   public class AppDbContext :DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Report> Reports { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


        }
    }
}
