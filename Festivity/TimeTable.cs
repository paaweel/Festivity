using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class TimeTable
    {
        public DateTime Start { get; private set;  }
        public DateTime End { get; private set; }

        public TimeTable(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }
     
        public TimeSpan GetDuration()
        {
            var span = End - Start;
            return span.Duration();
        }
    }
}
