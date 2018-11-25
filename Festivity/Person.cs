using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Person
    {
        public Person()
        {
            id = _id;
            ++_id;
        }

        private static int _id = 0;
        private int id;

        public List<TimeTable> Availability { get; set; }
        public List<TimeTable> Preferences { get; set; }
        
    }
}
