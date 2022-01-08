using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.MicroService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Data.Configurations
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.HasKey(x => x.UUID);
            builder.Property(x => x.UUID).UseIdentityColumn();
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Location).IsRequired().HasMaxLength(200);
            builder.HasOne<Person>(x => x.Person).WithMany(p => p.ContactInfos).HasForeignKey(c => c.PersonId);
            builder.ToTable("ContactInfos");
        }
    }
}

