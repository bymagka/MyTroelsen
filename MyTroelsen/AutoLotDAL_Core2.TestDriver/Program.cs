using System;
using System.Linq;
using AutoLotDAL_Core.DataInitialization;
using AutoLotDAL_Core.EF;
using AutoLotDAL_Core.Models;
using AutoLotDAL_Core.Repos;
using Microsoft.EntityFrameworkCore;

namespace AutoLotDAL_Core.TestDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("FUN WITH EF CORE");

            using (var context = new AutoLotEntities())
            {
                MyDataInitializer.RecreateDatabase(context);
                MyDataInitializer.InitializeData(context);

                foreach(Inventory inv in context.Cars)
                {
                    Console.WriteLine(inv);
                }
            }

            Console.WriteLine("Fun with repos");

            using (var repo = new InventoryRepo())
            {
                foreach(var c in repo.GetAll())
                {
                    Console.WriteLine(c);
                }
            }

            Console.ReadLine();
        }
    }
}
