using System;
using AutoLotDAL_Core.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AutoLotDAL_Core
{
    public partial class AutoLotEntities : DbContext
    {
        public DbSet<CreditRisks> CreditRisks { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Inventory> Cars { get; set; }

        public DbSet<Order> Orders { get; set; }


        public AutoLotEntities(DbContextOptions options)
            : base(options)
        {
        }


        internal AutoLotEntities()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = @"server=(localdb)\MSSQLLocalDB;database=AutoLotCore2;integrated security=True; MultipleActiveResultSets=True; App=EntityFramework;";
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
                              .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //составной индекс
            modelBuilder.Entity<CreditRisks>(entity =>
            {
                entity.HasIndex(e => new { e.FirstName, e.LastName }).IsUnique();
            });

            //каскадирование
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Car)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public string GetTableName(Type type)
        {
            return Model.FindEntityType(type).GetTableName();
        }
    }
}
