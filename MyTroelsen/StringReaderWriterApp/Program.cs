using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StringReaderWriterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with StringWriter / StringReader *****\n");

            using(StringWriter sw = new StringWriter())
            {
                sw.WriteLine("ASDasfsdf");

                Console.WriteLine(sw);


                StringBuilder sb = sw.GetStringBuilder();

                sb.Insert(0,"2 Hello");

                Console.WriteLine(sb.ToString());
                Console.WriteLine(sw);

                sb.Remove(0, "2 Hello".Length);


                Console.WriteLine(sb.ToString());
                Console.WriteLine(sw);

                using (StringReader sr = new StringReader(sw.ToString()))
                {
                    string input = null;

                    while((input = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(input);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
