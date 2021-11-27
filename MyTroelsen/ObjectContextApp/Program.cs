using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectContextApp
{
    class Program
    {
        static void Main(string[] args)
        {

            SportsCar sportsCar = new SportsCar();

            Console.WriteLine();

            SportsCar sportBus = new SportsCar();

            Console.WriteLine();

            SportsCarTS sychroSport = new SportsCarTS();
            Console.ReadLine();
        }
    }
}
