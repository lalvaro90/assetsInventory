using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsApi.Models
{
    public class AssetsContext : DbContext
    {
        public AssetsContext(DbContextOptions<AssetsContext> options)
              : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<AssetHistory> AssetsHistory { get; set; }
        public DbSet<Depreciation> Depreciations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Token>(entity => {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.Content);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.HasAlternateKey(e => e.Email);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.AssetId);
                entity.HasOne(e => e.Depreciation);
            });

            modelBuilder.Entity<AssetHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Depreciation>(entity => {
                entity.HasKey(e => e.Id);
            });
        }

    }
}
