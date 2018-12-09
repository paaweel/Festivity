using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Person
    {
        private static int _id = 0;
        public int Id { get; private set; }
        public List<TimeTable> Availability { get; private set; }
        public List<TimeTable> Preferences { get; private set; }

        public Person(List<TimeTable> availability, List<TimeTable> preferences = null)
        {
            Id = _id;
            _id++;
            if (availability != null) Availability = availability;
            if (preferences != null) Preferences = preferences;
    
        }      
    }
}
