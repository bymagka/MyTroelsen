using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace CustomSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Custom serializat");

            StringData myData = new StringData();

            SoapFormatter myFormatter = new SoapFormatter();

            using(Stream fStream = new FileStream("myData.soap",FileMode.Create,FileAccess.Write,FileShare.None))
            {
                myFormatter.Serialize(fStream, myData);
            }

            Console.ReadLine();
        }
    }


    [Serializable]
    public class StringData : ISerializable
    {
        string dataItemOne = "First data block";
        string dataItemTwo = "Second data block";


        public StringData()
        {

        }

        protected StringData(SerializationInfo info, StreamingContext context)
        {
            dataItemOne = info.GetString("First_Item").ToLower();
            dataItemTwo = info.GetString("dataItemTwo").ToLower();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("First_Item", dataItemOne.ToUpper());
            info.AddValue("dataItemTwo", dataItemTwo.ToUpper());
        }
    }
}
