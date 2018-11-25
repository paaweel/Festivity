using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Shift
    {
        //event - some label, way easier to just operate on shifts directly
        public TimeTable When { get; set; }
        public int Where { get; set; }
        public int PplNeeded { get; set; }
    }
}
