using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.MicroService.Core.Entity;


namespace MT.MicroService.Data.Configurations
{
    class ReportConfiguration:IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(x => x.UUID);
            builder.Property(x => x.UUID).UseIdentityColumn();
            builder.Property(x => x.ReportState).IsRequired();
            builder.Property(x => x.RequestDate).IsRequired();
           
            builder.ToTable("Reports");
        }
    }
  
}
