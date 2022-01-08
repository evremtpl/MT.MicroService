using Microsoft.EntityFrameworkCore;
using MT.MicroService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.MicroService.Data.FileContext
{
    class AppDbContext :DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Person>  Persons { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
