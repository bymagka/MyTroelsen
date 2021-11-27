using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.DataOperations;
using AutoLotDAL.Models;
using AutoLotDAL.BulkImport;

namespace AutoLotClient
{
    class Program
    {
        static void Main(string[] args)
        {
            InventoryDAL dal = new InventoryDAL();

            var list = dal.GetAllInventory();

            Console.WriteLine("****All Cars");

            Console.WriteLine("CarId\tMake\tColor\tPet Name");

            foreach(var item in list)
            {
                Console.WriteLine($"{item.CarId}\t{item.Make}\t{item.Color}\t{item.PetName}");
            }

            Console.WriteLine();

            var car = dal.GetCarById(list.OrderBy(x => x.Color)
                                         .Select(x => x.CarId)
                                         .First());

            Console.WriteLine("**** First by color*****");
            Console.WriteLine("CarId\tMake\tColor\tPet Name");
            Console.WriteLine($"{car.CarId}\t{car.Make}\t{car.Color}\t{car.PetName}");

            try
            {
                dal.DeleteeAuto(dal.GetCarById(5));
                Console.WriteLine("Car deleted!");
            }catch(Exception ex)
            {
                Console.WriteLine($"An exception occured: {ex.Message}");
            }

            dal.InsertAuto(new Car { Make = "Pilot", Color = "Blue", PetName = "TowMonster" });

            list = dal.GetAllInventory();

            var newCar = list.First(x=> x.PetName.Trim(' ') == "TowMonster");

            Console.WriteLine("**** New car *****");
            Console.WriteLine("CarId\tMake\tColor\tPet Name");
            Console.WriteLine($"{newCar.CarId}\t{newCar.Make}\t{newCar.Color}\t{newCar.PetName}");

            dal.DeleteeAuto(newCar);

            var petName = dal.GetCarById(newCar.CarId);

            Console.WriteLine("**** New car *****");
            Console.WriteLine($"Car pet name is {petName}!");
            Console.WriteLine("Press space for continue...");

            MoveCustomer(dal);
            DoBulkCopy();
            Console.ReadLine();
        }
    
        public static void MoveCustomer(InventoryDAL dal)
        {
            Console.WriteLine("**** Transaction example***");

            Console.WriteLine("Do you want to throw an exception? Y/N");

            string usrAnswer = Console.ReadLine();
            bool throwEx = true;

            if(usrAnswer.ToUpper() == "N")
            {
                throwEx = false;
            }

            dal.ProcessCreditRisk(throwEx, 1);

            Console.WriteLine("Check CreditRis");

            Console.ReadLine();
        }

        public static void DoBulkCopy()
        {
            Console.WriteLine("**** Do bulk copy********");

            var cars = new List<Car>
            {
                new Car(){Color="Blue",Make="Honda",PetName="MyCar1"},
                new Car(){Color="Blue",Make="Honda",PetName="MyCar2"},
                new Car(){Color="Blue",Make="Honda",PetName="MyCar3"},
                new Car(){Color="Blue",Make="Honda",PetName="MyCar4"}
            };

            ProcessBulkImport.ExecuteBulkImport(cars, "Inventory");

            InventoryDAL dal = new InventoryDAL();

            var list = dal.GetAllInventory();
            Console.WriteLine("CarID\tColor\tMake\tPetName");
            foreach(var item in list)
            {
                Console.WriteLine($"{item.CarId}\t{item.Color}\t{item.Make}\t{item.PetName}");
            }

            Console.WriteLine();
        }
    }
}
