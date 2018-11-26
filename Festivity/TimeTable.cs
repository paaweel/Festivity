using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class TimeTable
    {
        public TimeTable(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }
        public DateTime Start;
        public DateTime End;
        public TimeSpan GetDuration()
        {
            var span = End - Start;
            return span.Duration();
        }
    }
}
