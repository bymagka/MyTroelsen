using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TimerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Working with timer type");

            //delegate
            TimerCallback timeCB = new TimerCallback(PrintTime);

            //timer parameter
            var _ = new Timer(
                timeCB,
                123, //method arguments
                0,
                1000
                );

            Console.WriteLine("Hit key to terminate....");
            Console.ReadLine();
        }

        static void PrintTime(object state)
        {
            Console.WriteLine("Time is: {0}, Param is: {1}", DateTime.Now.ToLongTimeString(),state.ToString());
        }
    }
}
