using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StreamWriterReaderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with StreamWriter/StreamReader *****");

            using (StreamWriter writer = File.CreateText("reminders.txt"))
            {
                writer.WriteLine("Don't forget Mother's day this year");
                writer.WriteLine("Don't forget Father's day this year");
                writer.WriteLine("Don't forget yourself");

                writer.WriteLine("Don't forget these numbers:");

                for (int i = 0; i < 10; i++)
                    writer.WriteLine(i);

                writer.Write(writer.NewLine);
            }

            Console.WriteLine("File created");

            Console.WriteLine("Here are your thoughts");

            using(StreamReader reader = File.OpenText("reminders.txt"))
            {
                string input = null;

                while ((input = reader.ReadLine()) != null)
                {
                    Console.WriteLine(input);
                }
            }

            Console.ReadLine();
        }
    }
}
