using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MT.MicroService.FileCreateWorkerService.Models
{
    public partial class PhoneBookReportContext : DbContext
    {
        public PhoneBookReportContext()
        {
        }

        public PhoneBookReportContext(DbContextOptions<PhoneBookReportContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseNpgsql();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "English_United States.1254");

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Uuid);

                entity.Property(e => e.Uuid).HasColumnName("UUID");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
