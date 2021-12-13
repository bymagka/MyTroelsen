using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SimpleMultiThreadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with multi thread ****");

            Console.WriteLine("Do you want 1 or 2 threads?");

            string threadCount = Console.ReadLine();

            Thread currThread = Thread.CurrentThread;
            currThread.Name = "Primary";

            Console.WriteLine("{0} is executing Main()",Thread.CurrentThread.Name);

            Printer p = new Printer();

            switch (threadCount)
            {
                case "2":
                    {
                        Thread backgroundThread = new Thread(new ThreadStart(p.PrintNumbers));
                        backgroundThread.Name = "Secondary";
                        backgroundThread.Start();
                        break;
                    }

                case "1":
                    {
                        p.PrintNumbers();
                        break;
                    }
                default:
                    goto case "1";
            }

            MessageBox.Show("I`m  busy", "Work on main thread..");
            Console.ReadLine();
        }
    }
}
