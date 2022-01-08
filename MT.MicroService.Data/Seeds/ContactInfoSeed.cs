using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MT.MicroService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Data.Seeds
{
    class ContactInfoSeed : IEntityTypeConfiguration<ContactInfo>
    {

        private readonly int[] _ids;

        public ContactInfoSeed(int[] ids)
        {
            _ids = ids;

        }
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.HasData(new ContactInfo
            {
                UUID = 1,
                PhoneNumber = "05554443231",
                Email = "xyz@any.com",
                Location = "Ankara",
                PersonId = _ids[0]
            },
            new ContactInfo
            {
                UUID = 2,
                PhoneNumber = "05554443231",
                Email = "xyz@any.com",
                Location = "Ankara",
                PersonId = _ids[0]
            });
        }
    }
}
