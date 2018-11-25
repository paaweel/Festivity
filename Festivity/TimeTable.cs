using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class TimeTable
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan GetDuration()
        {
            var span = End - Start;
            return span.Duration();
        }
    }
}
