using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using AutoLotConsoleApplication.ef;


namespace AutoLotConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            WriteLine("*** Fun with ADO.NET EF ***");
            int carId = AddNewRecord();
            WriteLine(carId);
            PrintAllInventory();
            ReadLine();
        }


        private static int AddNewRecord()
        {
            using (var context = new AutoLotEntities())
            {
                try
                {
                    Car newCar = new Car() { Make = "Yugo", Color = "Brown", CarNickName = "Brownie" };
                    context.Cars.Add(newCar);
                    context.SaveChanges();
                    return newCar.CarId;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException?.Message);
                    return 0;
                }
            }
        }

        private static void PrintAllInventory()
        {
            using (var context = new AutoLotEntities())
            {
                foreach(var item in context.Cars.Where(x => x.Make == "BMW"))
                {
                    WriteLine(item);
                }
            }
        }
    }
}
