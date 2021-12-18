using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileStreamApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("**** Fun with filestream *****");

            using (FileStream fStream = File.Open(".\\myMessage.dat", FileMode.Create))
            {
                string msg = "Hello!";

                byte[] msgAsByteArray = Encoding.Default.GetBytes(msg);

                //Writing
                fStream.Write(msgAsByteArray, 0, msgAsByteArray.Length);

                fStream.Position = 0;

                Console.WriteLine("Your message as an array of bytes");

                byte[] bytesFromFile = new byte[msgAsByteArray.Length];

                for(int i = 0; i < msgAsByteArray.Length; i++)
                {
                    bytesFromFile[i] = (byte)fStream.ReadByte();
                    Console.WriteLine(bytesFromFile[i]);
                }

                Console.WriteLine("\nDecoded message");
                Console.WriteLine(Encoding.Default.GetString(bytesFromFile));
                
            }

            Console.ReadLine();
        }
    }
}
