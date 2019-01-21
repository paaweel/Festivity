using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Festival
    {
        public List<Shift> Shifts { get; set; }
        public List<Person> People { get; set; }
        public int[,] Locations;

        public Festival()
        {
            Console.WriteLine("Creating empty Festival");
            People = new List<Person>();
            Shifts = new List<Shift>();
            Locations = new int[3,3];
        }

        public Festival(List<Person> people, List<Shift> shifts, int[,] locations)
        {
            string errorMsg = "";
            if (people != null) People = people;
            else errorMsg = " people";

            if (shifts != null) Shifts = shifts;
            else errorMsg += " shifts";
            
            if (locations != null) Locations = locations;
            else errorMsg += " locations";

            if(errorMsg != "")
                Console.WriteLine("Values of" + errorMsg + " have not been set properly!");
        }

        
    }
}
