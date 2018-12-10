using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    interface IDataLoader
    {
        Festival LoadData();
        Festival LoadData(string source);
    }

    class DataLoaderConsole : IDataLoader
    {
        public Festival LoadData()
        {
            return new Festival();
        }
        public Festival LoadData(string source)
        {
            return new Festival();
        }
    }

    class DataLoaderJson : IDataLoader
    {
        public Festival LoadData()
        {
            Console.WriteLine("File not received! ");
            Console.WriteLine("Returning default class");
            return new Festival();
        }

        public Festival LoadData(string fileName)
        {
            List<Person> ppl;
            List<Shift> shifts;
            int[,] locations;
            using (StreamReader r = new StreamReader(fileName + "_ppl.json"))
            {
                string json = r.ReadToEnd();
                ppl = JsonConvert.DeserializeObject<List<Person>>(json);
            }
            using (StreamReader r = new StreamReader(fileName + "_shifts.json"))
            {
                string json = r.ReadToEnd();
                 shifts = JsonConvert.DeserializeObject<List<Shift>>(json);
            }
            using (StreamReader r = new StreamReader(fileName + "_locs.json"))
            {
                string json = r.ReadToEnd();
                locations = JsonConvert.DeserializeObject<int[,]>(json);
            }

            return new Festival(ppl, shifts, locations);
        }
    }

    class DataLoaderApp : IDataLoader
    {
        public Festival LoadData()
        {
            return new Festival();
        }

        public Festival LoadData(string source)
        {
            return new Festival();
        }

    }
}
