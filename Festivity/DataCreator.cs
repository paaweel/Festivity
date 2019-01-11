using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    public class DataCreator
    {
        private List<Person> ppl;
        private List<Shift> shifts;
        private Random random = new Random();

        public bool CreateAndSaveDataToFile(string fileName, int nOfPpl, int nOfShifts)
        {
            //TODO: divide this method
            ppl = new List<Person>();
            shifts = new List<Shift>();
            int days = 7;
            int location = 5;

            //people

            for (int i = 0; i < nOfPpl; ++i)
            {
                List<TimeTable> ava = new List<TimeTable>();
                for (int j = 0; j < 3; ++j)
                {
                    //TODO: check if they overlap
                    DateTime start = new DateTime(2018, 10, 1).AddHours(random.Next(days * 24 - 1));
                    DateTime end = start.AddHours(2);
                    ava.Add(new TimeTable(start, end));
                }

                ppl.Add(new Person(ava));
            }

            using (StreamWriter file = File.CreateText(fileName + "_ppl.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, ppl);
            }

            //shifts

            for (int i = 0; i < nOfShifts; ++i)
            {
                //TODO: check if they overlap
                DateTime start = new DateTime(2018, 10, 1).AddHours(random.Next(days * 24 - 1));
                DateTime end = start.AddHours(1);
                shifts.Add(new Shift(start, end, random.Next(5), random.Next(location), random.Next(5)));
            }

            using (StreamWriter file = File.CreateText(fileName + "_shifts.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, shifts);
            }

            //location
            int[,] locations = new int[location, location];

            for (int i = 0; i < location; ++i)
            {
                for (int j = i; j < location; j++)
                {
                    if (j == i)
                    {
                        locations[i,j] = 0;
                    }
                    else
                    {
                        locations[i, j] = random.Next(5, 60);
                        locations[j, i] = locations[i, j];
                    }
                }
            }

            using (StreamWriter file = File.CreateText(fileName + "_locs.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, locations);
            }

            return true;
        }
    }
}
