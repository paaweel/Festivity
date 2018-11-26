using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Shift
    {
        private static int _id = 0;
        public Shift(DateTime start, DateTime end, int pplNeeded, int location)
        {
            id = _id;
            _id++;
            this.PplNeeded = pplNeeded;
            this.When = new TimeTable(start, end);
            this.Where = location;

        }
        //event - some label, way easier to just operate on shifts directly
        public TimeTable When;
        public int Where { get; set; }
        public int PplNeeded { get; set; }
        public int id;
    }
}
