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
