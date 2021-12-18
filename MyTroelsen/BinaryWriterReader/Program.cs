using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BinaryWriterReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Binary Writer/Binary Reader");

            FileInfo fInfo = new FileInfo("BinFile.dat");

            using(BinaryWriter bw = new BinaryWriter(fInfo.OpenWrite()))
            {
                Console.WriteLine("Base stream: {0}", bw.BaseStream);


                double aDouble = 12345.67;
                int anInt = 123;
                string aString = "Test";


                bw.Write(aDouble);
                bw.Write(anInt);
                bw.Write(aString);
            }

            Console.WriteLine("Done");

            //Reading 
            using(BinaryReader br = new BinaryReader(fInfo.OpenRead()))
            {
                Console.WriteLine(br.ReadDouble());
                Console.WriteLine(br.ReadInt32());
                Console.WriteLine(br.ReadString());
            }

            Console.ReadLine();
        }
    }
}
