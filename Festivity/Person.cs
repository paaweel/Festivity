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
        public int id;

        public Person()
        {
            id = _id;
        }

        public List<TimeTable> Availability { get; set; }
        public List<TimeTable> Preferences { get; set; }
        
    }
}
