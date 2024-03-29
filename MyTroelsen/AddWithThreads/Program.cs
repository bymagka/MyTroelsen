﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AddWithThreads
{
    class Program
    {
        static AutoResetEvent waitHandle = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("**** Adding with Thread objects ****");
            Console.WriteLine("ID of thread in Main(): {0}", Thread.CurrentThread.ManagedThreadId);

            AddParams ap = new AddParams(10, 20);

            Thread d = new Thread(new ParameterizedThreadStart(Add));
            d.Start(ap);

            waitHandle.WaitOne();

            Console.WriteLine("Other thread is done");

            Console.ReadLine();

        }

        static void Add(object data)
        { 
            if(data is AddParams)
            {
                Console.WriteLine("ID of thread in Add(): {0}", Thread.CurrentThread.ManagedThreadId);

                AddParams ap = (AddParams)data;

                Console.WriteLine("{0} + {1} = {2}", ap.a, ap.b, ap.a + ap.b);

                Thread.Sleep(5000);

                waitHandle.Set();
            }
        }
    }
}
