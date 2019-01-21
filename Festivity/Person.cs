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
        //public List<TimeTable> Availability { get; private set; }
        public List<TimeTable> Preferences { get; private set; }

        public Person(List<TimeTable> preferences)
        {
            Id = _id;
            _id++;
            //if (availability != null) Availability = availability;
            if (preferences != null) Preferences = preferences;
    
        }

        public double CompareToPref(List<Shift> shifts)
        {
            double result;
            double returnValue = 0;
            result = Double.MaxValue;
            foreach (var shift in shifts)
            {
                result = Double.MaxValue;
                foreach (var pref in Preferences)
                {
                    result = pref.Contains(shift.When) >= result ? result : pref.Contains(shift.When);
                }
                returnValue += result;
            }
            return returnValue;
        }
    }
}
