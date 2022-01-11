using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace MT.ReportService.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = " Server = localhost; Port = 5432; Database = merverapor; User Id = postgres; Password = 123456; ";
            builder.UseNpgsql(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
