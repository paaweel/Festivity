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

        public double Contains(TimeTable value)
        {
            //checks if given value is in the span of a caller
            //0 if contains
            //+1 for each minute
            double result = 0;
            if (Start <= value.Start)
            {
                if (End >= value.End)
                {
                    result = 0;
                }
                else
                {
                    result = (value.End - End).TotalMinutes;
                }
            }
            else
            {
                result = (Start - value.Start).TotalMinutes;
            }

            return result;
        }
    }
}
