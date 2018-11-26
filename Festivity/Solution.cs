using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Solution
    {
        private List<Person> Ppl;
        private List<Shift> Shifts;
        public Dictionary<Person, List<Shift>> Assignment;
        public double? Fitness;

        public Solution(Festival festival)
        {
            this.Ppl = festival.People;
            this.Shifts = festival.Shifts;
            this.Assignment = new Dictionary<Person, List<Shift>>();

        }

        private Dictionary<Person, List<Shift>> CreateRandom()
        {
            do
            {
                var tempShifts = Shifts;
                for (int i = 0; i < Ppl.Count; i++)
                {
                    if (tempShifts.Count != 0)
                    {
                        Assignment[Ppl[i]].Add(Shifts[0]);
                        tempShifts.RemoveAt(0); 
                    }
                    
                }
            } while (!this.Validate());

            return new Dictionary<Person, List<Shift>>();
        }

        public bool Validate()
        {
            return true;
        }
        public double Evaluate(bool eval = true)
        {
            if (Fitness == null || eval)
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
