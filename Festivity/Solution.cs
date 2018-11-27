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
        private Random RandomGen;
        private bool RandomGenSet = false;

        public Solution(Festival festival, Random randomGen) : this(festival)
        {
            this.RandomGen = randomGen;
            this.RandomGenSet = true;
        }

        public Solution(Festival festival)
        {
            this.Ppl = festival.People;
            this.Shifts = festival.Shifts;
            this.Assignment = new Dictionary<Person, List<Shift>>();
            if (!RandomGenSet)
            {
                this.RandomGen = new Random();
            }
        }



        private void CreateRandom()
        {
            do
            {
                List<int> usedPpl = new List<int>();
                List<int> usedShifts = new List<int>();
                int pplIndex;
                int shiftsIndex;
                while (usedShifts.Count < Shifts.Count)
                {
                    //generate random indexes for ppl and shifts
                    do
                    {
                        pplIndex = RandomGen.Next(Ppl.Count - 1);
                    } while (usedPpl.Contains(pplIndex));
                    usedPpl.Add(pplIndex);

                    do
                    {
                        shiftsIndex = RandomGen.Next(Shifts.Count - 1);
                    } while (usedShifts.Contains(shiftsIndex)) ;

                    //Check if shifts have enough ppl
                    if (Shifts[shiftsIndex].PplNeeded > Shifts[shiftsIndex].PplAssigned)
                    {
                        Shifts[shiftsIndex].PplAssigned++;
                    }
                    else
                    {
                        usedShifts.Add(shiftsIndex);
                    }

                    Assignment[Ppl[pplIndex]].Add(Shifts[shiftsIndex]);
                }
            } while (!this.Validate());
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
