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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<AcquisitionMethod>  AcquisitionMethods { get; set; }

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

            modelBuilder.Entity<Person>(entity => {
                entity.HasKey(e => e.ID);
                entity.HasAlternateKey(e => e.Email);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.ID);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasKey(e => e.ID);
            });

            modelBuilder.Entity<AcquisitionMethod>(entity =>
            {
                entity.HasKey(e => e.ID);
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.AssetId);
                entity.HasOne(e => e.Depreciation);
                entity.HasOne(e => e.AcquisitionMethod);
                entity.HasOne(e => e.State);
                entity.HasOne(e => e.Responsible);
                entity.HasOne(e => e.Location);
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
