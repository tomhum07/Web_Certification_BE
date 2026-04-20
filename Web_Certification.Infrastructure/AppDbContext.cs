using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Web_Certification.Domain.Entities;

namespace Web_Certification.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Certificate> Certificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Code First: Cấu hình bảng
            modelBuilder.Entity<Certificate>().HasKey(c => c.Id);
            modelBuilder.Entity<Certificate>().HasIndex(c => c.CertHash).IsUnique(); // Hash phải là duy nhất
        }
    }
}
