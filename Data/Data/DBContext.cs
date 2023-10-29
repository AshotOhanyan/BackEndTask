using Data.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class DBContext : DbContext
    {
        public DBContext()
        {

        }
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = Environment.GetEnvironmentVariable("MY_DATABASE_CONNECTIONSTRING", EnvironmentVariableTarget.Machine);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

            optionsBuilder.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });

#if DEBUG
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
#endif

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(e => e.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Class>()
               .HasIndex(e => e.Name)
               .IsUnique();


            modelBuilder.Entity<Class>().HasData(
            new Class
            {
                Id = Guid.NewGuid(),
                Name = "Math"
            },
            new Class
            {
                Id = Guid.NewGuid(),
                Name = "Biology"
            },
            new Class
            {
                Id = Guid.NewGuid(),
                Name = "Astrology"
            }, 
            new Class
            {
                Id = Guid.NewGuid(),
                Name = "Physical education"
            });
        }
    }
}
