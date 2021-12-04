using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace SyncDelegateReview
{
    public delegate int BinaryOp(int x, int y);

    class Program
    {
        static bool isDone = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Main() invoked in thread {0}.", Thread.CurrentThread.ManagedThreadId);

            BinaryOp op = new BinaryOp(Add);

            IAsyncResult result = op.BeginInvoke(10, 10,new AsyncCallback(AddComplete), "Main thanks you for adding these  numbers");

            while(!isDone)
            {
                Console.WriteLine("Doing more work in Main()!");
                Thread.Sleep(1000);
            }
      
            Console.ReadLine();

        }


        static int Add(int x,int y)
        {
            Console.WriteLine("Add() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(5000);

            return x + y;
        }

        static void AddComplete(IAsyncResult ar)
        {
            AsyncResult result = (AsyncResult)ar;

            string message = (string)ar.AsyncState;

            BinaryOp op = (BinaryOp)result.AsyncDelegate;

            Console.WriteLine("10 + 10 is {0}.", op.EndInvoke(ar));

            Console.WriteLine(message);

            Console.WriteLine("AddComplete() invoked on thread {0}.", Thread.CurrentThread.ManagedThreadId);

            isDone = true;

        }
    }
}
