using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Program
    {
        static void Main(string[] args)
        {
            DataCreator d = new DataCreator();
            d.CreateAndSaveDataToFile("test", 3, 3);
            IDataLoader l = new DataLoaderJson();
            Festival f = l.LoadData("test");

        //    Console.WriteLine(f.People[].Id);

            Console.ReadKey();
            //TimeTable t1 = new TimeTable();


            //          Festival festival = new Festival();
            //        EAAlgorithm alg = new EAAlgorithm(festival,5);
            //      int i = 0;
            //    foreach (var sol in alg.Population)
            //  {
            //    i++;
            //  Console.WriteLine($"\n{i}th solution");
            //foreach (KeyValuePair<Person, List<Shift>> entry in sol.Assignment)
            //{
            //  Console.WriteLine($"Person: {entry.Key.id} has following shifts assigned:");
            //foreach (var shift in entry.Value)
            //{
            //  Console.Write(shift.id);
            //Console.Write(" ");
            //}
            //Console.WriteLine("");
            //}
            //}
            //Console.ReadKey();
            /*
            Locations = new int[3, 3] {
                {0, 10, 20},
                {20, 0, 10},
                {10, 20, 0}
            };

            People = new List<Person>();

            People.Add(new Person());
            People.Add(new Person());
            People.Add(new Person());

            Shifts = new List<Shift>();

            Shifts.Add(new Shift(new DateTime(2018, 10, 10, 10, 10, 10, 10),    //start
                                 new DateTime(2018, 10, 10, 11, 10, 10, 10),    //end
                                 3,                                             //pplNeeded
                                 1,                                             //loc
                                 0));

            Shifts.Add(new Shift(new DateTime(2018, 10, 11, 10, 10, 10, 10),
                                 new DateTime(2018, 10, 11, 13, 10, 10, 10),
                                 2,
                                 0,
                                 1));

            Shifts.Add(new Shift(new DateTime(2018, 10, 10, 11, 10, 10, 10),
                                 new DateTime(2018, 10, 10, 12, 10, 10, 10),
                                 1,
                                 2,
                                 2));
        */
        }

    }
}
