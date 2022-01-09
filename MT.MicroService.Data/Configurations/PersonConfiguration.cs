using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.MicroService.Core.Entity;


namespace MT.MicroService.Data.Configurations
{
    class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.UUID);
            builder.Property(x => x.UUID).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x=>x.SurName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Company).IsRequired().HasMaxLength(200);
            builder.ToTable("Persons");
        }
    }
}
