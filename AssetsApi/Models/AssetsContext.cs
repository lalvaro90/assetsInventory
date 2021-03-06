﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<AssetNotes> AssetNotes { get; set; }
        public DbSet<Provider> Providers { get; set; }

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
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasOne(e => e.Responsible1);
                entity.HasOne(e => e.Responsible2);
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasKey(e => e.ID);
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(e => e.ID);
            });

            modelBuilder.Entity<AcquisitionMethod>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasIndex(e => e.ID);
            });

            modelBuilder.Entity<AssetNotes>(entity => {
                entity.HasKey(e => e.IdNote);
                entity.HasOne(e => e.Asset);
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
                entity.HasOne(e => e.Provider);
            });

            modelBuilder.Entity<AssetHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Depreciation>(entity => {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Configuration>(entity => {
                entity.HasKey(e => e.IdConfig);
            });
        }

    }
}
