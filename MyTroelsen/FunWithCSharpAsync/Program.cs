using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FunWithCSharpAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Fun with async");

            List<int> l = default;

            Console.WriteLine(DoWork());
            Console.WriteLine("Completed");

            Console.ReadLine();
        }

        static string DoWork()
        {
            Thread.Sleep(5_000);
            return "Done with work";
        }
    }
}
