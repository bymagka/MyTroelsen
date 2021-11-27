using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AutoLotConsoleApplication.ef
{
    public partial class AutoLotEntities : DbContext
    {
        public AutoLotEntities()
            : base("name=AutoLotEntities")
        {
        }

        public virtual DbSet<CreditRisks> CreditRisks { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.Make)
                .IsFixedLength();

            modelBuilder.Entity<Car>()
                .Property(e => e.Color)
                .IsFixedLength();

            modelBuilder.Entity<Car>()
                .Property(e => e.CarNickName)
                .IsFixedLength();

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);
        }
    }
}
