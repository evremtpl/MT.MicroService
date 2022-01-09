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
    public class PersonSeed : IEntityTypeConfiguration<Person>
    {
        private readonly int[] _ids;

        public PersonSeed(int[] ids)
        {
            _ids = ids;

        }
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData(new Person
            {
                UUID = _ids[0],
                Name = "Demir",
                SurName = "Çelik",
                Company = "xyz"

            });
      
        }
    }
}
