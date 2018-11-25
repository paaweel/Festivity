using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Solution
    {
        public Dictionary<Person, Shift> Assignment;
        public double? Fitness;
        public bool Validate()
        {
            return true;
        }
        public double Evaluate()
        {
            if (Fitness == null)
            {
                // calculate fitness
            }

            return (double)Fitness;
        }

    }
}
