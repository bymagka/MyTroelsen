using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAppDomainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InitDAD();
            DisplayDADStats();
            ListAllAssemblies();
            Console.ReadLine();
        }

        public static void DisplayDADStats()
        {
            AppDomain defaultDomain = AppDomain.CurrentDomain;

            Console.WriteLine("Name of this domain: {0}", defaultDomain.FriendlyName);

            Console.WriteLine("Id of this domain: {0}", defaultDomain.Id);

            Console.WriteLine("Is this the default domain? : {0}", defaultDomain.IsDefaultAppDomain());

            Console.WriteLine("Base directory of this domain : {0}", defaultDomain.BaseDirectory);

            Console.WriteLine("Identity of this domain: {0}", defaultDomain.ApplicationIdentity);

            Console.WriteLine("Context of this domain: {0}", defaultDomain.ActivationContext);

            Console.WriteLine("App trust of this domain: {0}", defaultDomain.ApplicationTrust);

            Console.WriteLine("Setup information of this domain: {0}", defaultDomain.SetupInformation);

            Console.WriteLine("------------------------");

            Console.WriteLine();
        }
    
    
        public static void ListAllAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x=>x.GetName().Name);

            foreach(var assembly in assemblies)
            {
                Console.WriteLine("Assembly name: {0}", assembly.GetName().Name);
                Console.WriteLine("Assembly version: {0}", assembly.GetName().Version);
            }
        }
    
        private static void InitDAD()
        {
            AppDomain.CurrentDomain.AssemblyLoad += (send, args) =>
            {
                Console.WriteLine(((AppDomain)send).FriendlyName);

                Console.WriteLine($"{args.LoadedAssembly.GetName().Name} has been loaded");
            };
        }
    }
}
