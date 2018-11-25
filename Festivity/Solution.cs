using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Solution
    {
        public Dictionary<Person, List<Shift>> Assignment;
        public double? Fitness;
        public bool Validate()
        {
            return true;
        }
        public double Evaluate()
        {
            if (Fitness == null)
            {
                // calculate Fitness
                // 1 - calculate avarege load
                int nOfPeople = Assignment.Count;
                int totalLoad = 0;
                foreach (KeyValuePair<Person, List<Shift>> entry in Assignment)
                {
                    totalLoad += entry.Value.Sum(item => item.When.GetDuration().Minutes);                 
                }
                double averageLoad = totalLoad / nOfPeople;


                
                Fitness = 0;
                foreach(KeyValuePair<Person, List<Shift>> entry in Assignment)
                {
                    int load = entry.Value.Sum(item => item.When.GetDuration().Minutes);
                    foreach (var shift in entry.Value)
                    {
                        Fitness += Math.Sqrt(Math.Pow(load - averageLoad, 2.00));
                    }
                }
            }

            return (double)Fitness;
        }

    }
}
