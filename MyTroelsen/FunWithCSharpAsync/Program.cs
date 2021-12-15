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

            

            string message = await DoWorkAsync();

            await MethodReturningVoidAsync();
            Console.WriteLine("Void method complete");

            Console.WriteLine(message);
            Console.WriteLine("Completed");

            Console.ReadLine();
        }

        static string DoWork()
        {
            Thread.Sleep(5_000);
            return "Done with work";
        }

        static async Task<string> DoWorkAsync()
        {
            return await Task.Run(() =>
            {
                return DoWork();
            });
        }

        static async Task MethodReturningVoidAsync()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(4_000);
            });

            Console.WriteLine("Void method completed");
        }
    }
}
