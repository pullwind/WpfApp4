using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfApp4
{
    class SaveToFile
    {
      public  static void SaveViaDataContractSerialization<T>(T serializableObject, string filepath)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
            };

            var writer = XmlWriter.Create(filepath, settings);
            serializer.WriteObject(writer, serializableObject);
            writer.Close();
        }

      public  static T LoadViaDataContractSerialization<T>(string filepath)
        {
            var fileStream = new FileStream(filepath, FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
            var serializer = new DataContractSerializer(typeof(T));
            T serializableObject = (T)serializer.ReadObject(reader, true);
            reader.Close();
            fileStream.Close();

            return serializableObject;
        }

        /*
        static void Main(string[] args)
        {
            // Save single object
            Car bmw = new Car("BMW", 200, new List<string> { "Left", "Right" });    // create object
            SaveViaDataContractSerialization(bmw, "bmw.xml");                       // save object
            bmw = null;                                                             // delete object
            bmw = LoadViaDataContractSerialization<Car>("bmw.xml");                 // reload object
            Console.WriteLine(bmw.ToString());                                      // print object



            // Save list of objects
            List<Car> carList = new List<Car>                                       // create object list
            {
                new Car("Porsche", 250, new List<string> { "Left" }),
                new Car("Mercedes", 150, new List<string> { "Front", "Back" }),
                new Car("Aston Martin", 300, new List<string> { "Front" })
            };
            SaveViaDataContractSerialization(carList, "cars.xml");                  // save object list
            carList = null;                                                         // delete object list
            carList = LoadViaDataContractSerialization<List<Car>>("cars.xml");      // reload object list
            foreach (var a in carList)                                              // print object list
                Console.WriteLine(a.ToString());

            Console.ReadLine();
        }
        */
    }
}
