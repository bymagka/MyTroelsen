using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace SimpleSerialize
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("****** Fun with Object Serializable ******");

            JamesBondCar jbc = new JamesBondCar();
            jbc.canFly = true;
            jbc.canSubmerge = false;
            jbc.theRadio.stationPresets = new double[] {89.3,105.1,97.1 };
            jbc.theRadio.hasTweeters = true;


            SaveAsBinaryFormat(jbc,"CarData.dat");
            ReadAsBinaryFormat("CarData.dat");
            SaveAsSoapFormat(jbc, "CarData.dat");
            ReadAsSoapFormat("CarData.dat");
            SaveAsXML(jbc, "CarData.xml");
            SaveListAsXML("CarDataList.xml");

            Console.ReadLine();
        }

        private static void SaveAsBinaryFormat(object objGraph, string filename)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using(FileStream fStream = new FileStream(filename,FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }

            Console.WriteLine("Save data in binary format");
        }

        private static void SaveAsSoapFormat(object objGraph, string filename)
        {
            SoapFormatter binFormat = new SoapFormatter();

            using (FileStream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }

            Console.WriteLine("Save data in binary format");
        }
        private static void ReadAsSoapFormat(string filename)
        {
            SoapFormatter binFormat = new SoapFormatter();

            using (FileStream fStream = File.OpenRead(filename))
            {
                var car = binFormat.Deserialize(fStream);
                Console.WriteLine("Read: {0}", car.ToString());
            }


        }

        private static void ReadAsBinaryFormat(string filename)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (FileStream fStream = File.OpenRead(filename))
            {
                var car = binFormat.Deserialize(fStream);
                Console.WriteLine("Read: {0}",car.ToString());
            }

           
        }
    
        private static void SaveAsXML(object objGraph, string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(JamesBondCar));

            using(Stream fStream = new FileStream(filename,FileMode.Create,FileAccess.Write,FileShare.None))
            {
                serializer.Serialize(fStream, objGraph);
            }

            Console.WriteLine("Save in XML");
        }

    

        private static void SaveListAsXML(string filename)
        {
            List<JamesBondCar> jamesBondCars = new List<JamesBondCar>();
            jamesBondCars.Add(new JamesBondCar(true, true));
            jamesBondCars.Add(new JamesBondCar(false, true));
            jamesBondCars.Add(new JamesBondCar(true, false));
            jamesBondCars.Add(new JamesBondCar(false, false));

            XmlSerializer serializer = new XmlSerializer(typeof(List<JamesBondCar>));

            using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                serializer.Serialize(fStream, jamesBondCars);
            }

            Console.WriteLine("List saved in xml");
        }
    }



    [Serializable]
    public class Radio
    {
        public bool hasTweeters;
        public bool hasSubWoofers;
        public double[] stationPresets;

        [NonSerialized]
        public string radioID = "XF-552RR6";
    }


    [Serializable]
    public class Car
    {
        public Radio theRadio = new Radio();
        public bool isHatchback;

    }

    [Serializable, XmlRoot(Namespace = "test")]
    public class JamesBondCar : Car
    {
        [XmlAttribute]
        public bool canFly;

        [XmlAttribute]
        public bool canSubmerge;

        public JamesBondCar(bool skyWorthy,bool seaWorthy)
        {
            this.canFly = skyWorthy;
            this.canSubmerge = seaWorthy;
        }

        public JamesBondCar()
        {

        }


    }
}
