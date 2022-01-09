using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.MicroService.Core.Entity;


namespace MT.MicroService.Data.Configurations
{
    class ReportConfiguration:IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.ReportState).IsRequired();
            builder.Property(x => x.RequestDate).IsRequired();
           
            builder.ToTable("Reports");
        }
    }
  
}
