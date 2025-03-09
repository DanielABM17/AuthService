using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AuthService.Data
{
    public class OticaContext : DbContext
    {
        public OticaContext(DbContextOptions<OticaContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

            modelBuilder.Entity<Store>()
            .HasIndex(s => s.StoreNumber)
            .IsUnique();
        }
    }
}