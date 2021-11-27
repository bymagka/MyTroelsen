using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectContextApp
{
    public class SportsCar
    {
        

        public SportsCar()
        {
            Context ctx = Thread.CurrentContext;

            Console.WriteLine("{0} object in context {1}",this.ToString(),ctx.ContextID);

            foreach(IContextProperty prop in ctx.ContextProperties)
            {
                Console.WriteLine("-> Ctx prop: {0}", prop.Name);
            }
        }
    }
}
