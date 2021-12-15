using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirectoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fun with directories");

            ShowWindowsDirectoryInfo();
            DisplayFiles();
            ModifyAppDirectory();
            FunWithDirectoryType();

            Console.ReadLine();
        }

        static void ShowWindowsDirectoryInfo()
        {
            DirectoryInfo dir = new DirectoryInfo(@"D:\Загрузки");
            Console.WriteLine("******* Directory info ******");
            Console.WriteLine("FullName: {0}",dir.FullName);
            Console.WriteLine("Name: {0}", dir.Name);
            Console.WriteLine("Parent: {0}", dir.Parent);
            Console.WriteLine("Creation: {0}", dir.CreationTime);
            Console.WriteLine("Attributes: {0}", dir.Attributes);
            Console.WriteLine("Root: {0}", dir.Root);
        }



        static void DisplayFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(@"D:\Загрузки");

            FileInfo[] files = dir.GetFiles("*.pdf", SearchOption.AllDirectories);

            Console.WriteLine("******* Files info ******");


            foreach(FileInfo file in files)
            {
                Console.WriteLine("********");
             
                Console.WriteLine("Name: {0}", file.Name);
                Console.WriteLine("File size: {0}", file.Length);
                Console.WriteLine("Creation: {0}", file.CreationTime);
                Console.WriteLine("Attributes: {0}", file.Attributes);
                Console.WriteLine("Root: {0}", dir.Root);

                Console.WriteLine("********\n");
            }

          
        }

        static void ModifyAppDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(".");

            dir.CreateSubdirectory("MyFolder");

            DirectoryInfo myDataFolder = dir.CreateSubdirectory(@"MyFolder2\Data");

            Console.WriteLine("New folder is: {0}", myDataFolder);
        }


        static void FunWithDirectoryType()
        {
            string[] drivers = Directory.GetLogicalDrives();

            Console.WriteLine("Here are your drives:");

            foreach (string s in drivers)
                Console.WriteLine("---> {0} ", s);

            Console.WriteLine("Press Enter to delete directories");
            Console.ReadLine();

            try
            {
                Directory.Delete(".\\MyFolder");
                Directory.Delete(".\\MyFolder2",true);
            }
            catch (IOException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
