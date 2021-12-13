using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadStats
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread currThread = Thread.CurrentThread;

            currThread.Name = "ThePrimaryThread";

            Console.WriteLine($"Name of current domain {Thread.GetDomain().FriendlyName}");

            Console.WriteLine($"ID of current context {Thread.CurrentContext.ContextID}");

            Console.WriteLine($"Thread name {currThread.Name}");

            Console.WriteLine($"Has thread started?: {currThread.IsAlive}");

            Console.WriteLine($"Priority level {currThread.Priority}");

            Console.WriteLine($"Thread state {currThread.ThreadState}");

            Console.ReadLine();
        }
    }
}
