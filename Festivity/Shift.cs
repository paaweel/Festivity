using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Shift
    {
        public int EventId { get; private set; }
        public int Id { get; private set; }
        public TimeTable When { get; set; }
        public int Where { get; private set; }
        public int PplNeeded { get; private set; }
        public int PplAssigned { get; set; }

        private static int _id = 0;

        public Shift(DateTime start, DateTime end, int pplNeeded, int location, int eventId) 
        {
            EventId = eventId;
            Id = _id;
            _id++; //id == current number of shifts, incremented every time a shift is added 
            PplNeeded = pplNeeded;
            When = new TimeTable(start, end);
            Where = location;
            PplAssigned = 0;
        }
    }
}
