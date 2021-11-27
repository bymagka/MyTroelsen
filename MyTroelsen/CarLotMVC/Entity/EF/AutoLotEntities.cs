using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using AutoLotDAL.Models;
using System.Data.Entity.Infrastructure.Interception;
using AutoLotDAL.Interception;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace AutoLotDAL.EF
{
    public partial class AutoLotEntities : DbContext
    {
        //static readonly DatabaseLogger dbLogger = new DatabaseLogger("sqlLog.txt", true);

        public AutoLotEntities()
            : base("name=AutoLotConnection")
        {
            //DbInterception.Add(new ConsoleWriterInterception());
            //dbLogger.StartLogging();
            //DbInterception.Add(dbLogger);
            var context = (this as IObjectContextAdapter).ObjectContext;
            context.ObjectMaterialized += Context_ObjectMaterialized;
            context.SavingChanges += Context_SavingChanges;
        }

        private void Context_SavingChanges(object sender, EventArgs e)
        {
            var context = sender as ObjectContext;
            if (context == null) return;

            foreach(ObjectStateEntry item in context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added))
            {
                if((item.Entity as Inventory) != null)
                {
                    var entity = item.Entity as Inventory;
                    if(entity.Color == "Red")
                    {
                        item.RejectPropertyChanges(nameof(entity.Color));
                    }
                }
            }
        }

        private void Context_ObjectMaterialized(object sender, System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs e)
        {
            
        
        }

        public virtual DbSet<CreditRisks> CreditRisks { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Cars { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>()
                .Property(e => e.Make)
                .IsFixedLength();

            modelBuilder.Entity<Inventory>()
                .Property(e => e.Color)
                .IsFixedLength();

            modelBuilder.Entity<Inventory>()
                .Property(e => e.PetName)
                .IsFixedLength();

            modelBuilder.Entity<Inventory>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);
        }


    }
}
