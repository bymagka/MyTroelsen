using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.EF;
using AutoLotDAL.Models;
using System.Data.Entity;
using AutoLotDAL.Repos;
using System.Data.Entity.Infrastructure;

namespace AutoLotTestDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MyDataInitializer());

            Console.WriteLine("**** Fun with EF *******");

            using(AutoLotEntities context = new AutoLotEntities())
            {
                context.Cars.ToList().ForEach(car => Console.WriteLine(car));
            }
            Console.ReadLine();

            Console.WriteLine("**** Fun with repos ****");


            AddNewRecord(new Inventory() { Color = "Green", PetName = "Testy", Make = "Testy" });
            UpdateRecord(10);
          
            using(InventoryRepo ir = new InventoryRepo())
            {
                ir.GetAll().ForEach(x => Console.WriteLine(x));
            }

            TestConcurrency();
            Console.ReadLine();
        }


        private static void AddNewRecord(Inventory car)
        {
            using(var ir = new InventoryRepo())
            {
                ir.Add(car);
            }
        }

        private static void UpdateRecord(int carId)
        {
            using(var ir = new InventoryRepo())
            {
                var entity = ir.GetOne(carId);

                if (entity == null) return;

                entity.Color = "TestyChange";
                ir.Save(entity);
            }
        }

        private static void RemoveRecordByCar(Inventory car)
        {
            using(var ir = new InventoryRepo())
            {
                ir.Delete(car);
            }
        }

        private static void RemoveRecordById(int CarId,byte[] timeStamp)
        {
            using (var ir = new InventoryRepo())
            {
                ir.Delete(CarId, timeStamp);
            }
        }

        private static void TestConcurrency()
        {
            var repo1 = new InventoryRepo();
            var repo2 = new InventoryRepo();

            var car1 = repo1.GetOne(1);
            var car2 = repo2.GetOne(1);

            car1.Color = "Testy";
            repo1.Save(car1);

            car2.Color = "Yellow";

            try
            {
                repo2.Save(car2);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;
                var dbValues = entry.GetDatabaseValues();

                Console.WriteLine("****Concurrency****");
                Console.WriteLine("Type:\tColor");
                Console.WriteLine($"Current:\t{currentValues[nameof(Inventory.Color)]}");
                Console.WriteLine($"Orig:\t{originalValues[nameof(Inventory.Color)]}");
                Console.WriteLine($"Db:\t{dbValues[nameof(Inventory.Color)]}");
            }
        }
    }
}
