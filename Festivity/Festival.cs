using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Festival
    {
        public Festival()
        {
            Locations = new int[3, 3] {
                {0, 10, 20},
                {20, 0, 10},
                {10, 20, 0}
            };
            Shifts.Add(new Shift(new DateTime(2018, 10, 10, 10, 10, 10, 10),    //start
                                 new DateTime(2018, 10, 10, 11, 10, 10, 10),    //end
                                 3,                                             //pplNeeded
                                 1));                                           //loc

            Shifts.Add(new Shift(new DateTime(2018, 10, 11, 10, 10, 10, 10),    //start
                                 new DateTime(2018, 10, 11, 13, 10, 10, 10),    //end
                                 2,                                             //pplNeeded
                                 0));                                           //loc

            Shifts.Add(new Shift(new DateTime(2018, 10, 10, 11, 10, 10, 10),    //start
                                 new DateTime(2018, 10, 10, 12, 10, 10, 10),    //end
                                 1,                                             //pplNeeded
                                 2));                                           //loc
        }
        public List<Shift> Shifts;
        public List<Person> People;
        public int[,] Locations;
        public TimeSpan GetTravelTime(int Location1, int Location2)
        {
            TimeSpan TravelTime = new TimeSpan(0, Locations[Location1, Location2], 0);
            return TravelTime;
        }
    }
}
