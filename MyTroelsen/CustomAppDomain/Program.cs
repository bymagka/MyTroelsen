using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAppDomain
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain defaultDomain = AppDomain.CurrentDomain;

            ListAllAssemblies(defaultDomain);

            defaultDomain.ProcessExit += (s, arg) =>
            {
                Console.WriteLine("Default domain unloaded");
            };

            MakeNewAppDomain();

            AppDomain.Unload(defaultDomain);

            Console.ReadLine();
        }

        private static void LoadAssembly(AppDomain newDomain)
        {
            try
            {
                newDomain.Load("CarLotMVC");
            }
            catch (FileNotFoundException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static void ListAllAssemblies(AppDomain ad)
        {
            var assemblies = ad.GetAssemblies().OrderBy(x => x.GetName().Name);

            Console.WriteLine("Assemblies in domain {0}", ad.FriendlyName);

            foreach (var assembly in assemblies)
            {
                Console.WriteLine("Assembly name: {0}", assembly.GetName().Name);
                Console.WriteLine("Assembly version: {0}", assembly.GetName().Version);
            }
        }

        static void MakeNewAppDomain()
        {
            AppDomain newDomain = AppDomain.CreateDomain("SecondAppDomain");

            newDomain.DomainUnload += (s, arg) =>
            {
                Console.WriteLine($"Second domain has been unloaded!");
            };

            LoadAssembly(newDomain);
            ListAllAssemblies(newDomain);

            AppDomain.Unload(newDomain);
        }
    }
}
