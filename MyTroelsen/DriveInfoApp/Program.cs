using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DriveInfoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****** Fun with DriveInfo *****\n");

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach(DriveInfo drive in drives)
            {
                Console.WriteLine("Name: {0}", drive.Name);
                Console.WriteLine("Type: {0}", drive.DriveType);

                if (drive.IsReady)
                {
                    Console.WriteLine("Free space: {0}", drive.TotalFreeSpace);
                    Console.WriteLine("Format: {0}", drive.DriveFormat);
                    Console.WriteLine("Label: {0}", drive.VolumeLabel);

                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }


    }
}
