using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MultiThreadedPrinting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's fun");

            Printer p = new Printer();

            Thread[] threads = new Thread[5];

            for(int i = 0; i < 5; i++)
            {
                threads[i] = new Thread(p.PrintNumbers)
                {
                    Name = $"thread {i}",
                };
            }

            foreach (var thread in threads)
                thread.Start();

            Console.ReadLine();


            //second way
            WaitCallback waitCallback = new WaitCallback(PrintTheNumbers);

            for(int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(waitCallback, p);
            }

            Console.ReadLine();

        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state;

            task.PrintNumbers();
        }
    }
}
