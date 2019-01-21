using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
     class Solution
    {
        private List<Person> Ppl;
        private List<Shift> Shifts;
        private int [,] Loc;
        public Dictionary<Person, List<Shift>> Assignment;
        public double FitnessPref { private set; get; }
        public double FitnessLoad { private set; get; }
        private double? Fitness;
        static public int InvalidDueToLocalization = 0;
        static public int InvalidDueToWorkingOver6Day = 0;
        static public int InvalidDueToShiftsOverlap = 0;
        static public int AllChecked = 0;
        static private Random RandomGen = new Random();

        public Solution(Festival festival, Random randomGen = null)
        {
            Ppl = festival.People;
            Shifts = festival.Shifts;
            Loc = festival.Locations;
            Assignment = new Dictionary<Person, List<Shift>>();
            if (randomGen != null) RandomGen = randomGen;
            else RandomGen = new Random();
            Fitness = null;
            foreach (var person in Ppl)
            {
                Assignment.Add(person, new List<Shift>());
            }
            CreateRandom();
        }

        public Solution (Solution sol)
        {
            Ppl = sol.Ppl;
            Shifts = sol.Shifts;
            Assignment = sol.Assignment.ToDictionary(   entry => entry.Key,
                                                        entry => entry.Value);
        }

        public bool Assign(int pplIndex, int shiftsIndex)
        {
            //TODO: check if exeeds dimensions
            return Assign(Ppl[pplIndex], Shifts[shiftsIndex]);
        }

        public bool Assign(Person person, Shift shift)
        {
            if (!Assignment.ContainsKey(person))
            {
                Assignment.Add(person, new List<Shift>());
            }
            if (!Assignment[person].Contains(shift))
                Assignment[person].Add(shift);
            return true;
        }

        public bool Assign(Person person, List<Shift> shifts, bool exchange = false)
        {
            bool ret = true;
            if (exchange == false)
            {
                foreach (var shift in shifts)
                {
                    if (Assign(person, shift) != true)
                    {
                        ret = false;
                    }
                }
            }
            else
            {
                Assignment[person] = shifts;
            }
            
            return ret;
        }

        public void CreateRandom()
        {
            do
            {
                List<int> usedShifts = new List<int>();
                int pplIndex;
                int shiftsIndex;
                while (usedShifts.Count <= Shifts.Count)
                {
                    pplIndex = RandomGen.Next(Ppl.Count);

                    do
                    {
                        shiftsIndex = RandomGen.Next(Shifts.Count);
                    } while (usedShifts.Contains(shiftsIndex) && usedShifts.Count < Shifts.Count);
                    usedShifts.Add(shiftsIndex);

                    this.Assign(pplIndex, shiftsIndex);

                }
            } while (!Validate());
            Console.WriteLine("GOT OUT CR---------");
            this.Evaluate();
        }
       
        public bool Validate()
        {
            Console.WriteLine(AllChecked - InvalidDueToLocalization - InvalidDueToShiftsOverlap - InvalidDueToWorkingOver6Day);
            //sort shifts
            foreach (var entry in Assignment) 
            {
                
                entry.Value.Sort((x, y) => x.When.Start.CompareTo(y.When.Start));
                if (entry.Value.Count == 0)
                {
                    continue;
                }
                bool repairAttempted = false;
                
                int CurrentDay = entry.Value[0].When.Start.Day;
                int NextDay;
                double HoursPerDay = 0.0;
                for (int i = 0; i < entry.Value.Count -1; ++i)
                {
                    
                    AllChecked++;
                    //Overlapping
                    if (entry.Value[i].When.End > entry.Value[i + 1].When.Start)
                    {
                        InvalidDueToShiftsOverlap++;
                        if (!repairAttempted && entry.Value.Count > i+1)
                        {
                            entry.Value.Remove(entry.Value[i + 1]);
                            repairAttempted = true;
                        }
                        else
                        {
                            //Console.WriteLine("InvalidDueToShiftsOverlap: \t" + (InvalidDueToShiftsOverlap).ToString() +" out of " + AllChecked.ToString());
                            return false;
                        }
                        
                    }
                    
                    //Location
                    if (entry.Value[i].When.End + GetTravelTime(entry.Value[i].EventId, entry.Value[i+1].EventId) > entry.Value[i + 1].When.Start)
                    {
                        InvalidDueToLocalization++;
                        if (!repairAttempted && entry.Value.Count > i + 1)
                        {
                            entry.Value.Remove(entry.Value[i + 1]);
                            repairAttempted = true;
                        }
                        else 
                        {
                            //Console.WriteLine("InvalidDueToLocalization: \t" + (InvalidDueToLocalization).ToString() + " out of " + AllChecked.ToString());
                            return false;
                        }
                        
                    }

                    //Over Six a Day
                    NextDay = entry.Value[i+1].When.Start.Day;
                    if (CurrentDay == NextDay)
                    {
                        HoursPerDay += entry.Value[i].When.GetDuration().Hours;
                        HoursPerDay += entry.Value[i].When.GetDuration().Minutes/60;
                    } 
                    else
                    {
                        CurrentDay = NextDay;
                        if (HoursPerDay > 6)
                        {
                            InvalidDueToWorkingOver6Day++;
                            //Console.WriteLine("InvalidDueToWorkingOver6Day: \t" + (InvalidDueToWorkingOver6Day).ToString() + " out of " + AllChecked.ToString());
                            return false;
                        }
                        HoursPerDay = 0.0;
                    }
                }
            }
            return true;
        }

        public double Evaluate(bool eval = false)
        {
            if (Fitness == null || eval)
            {
                // calculate Fitness
                Fitness = 0;

                // 1 - calculate avarege load
                int nOfPeople = Assignment.Count;
                double totalLoad = 0;
                foreach (KeyValuePair<Person, List<Shift>> entry in Assignment)
                {
                    totalLoad += entry.Value.Sum(item => item.When.GetDuration().TotalMinutes);
                }
                double averageLoad = totalLoad / nOfPeople;
                double load;

                FitnessLoad = 0.0;
                FitnessPref = 0.0;

                foreach (KeyValuePair<Person, List<Shift>> entry in Assignment)
                {
                    //calculate load of a given person
                    load = entry.Value.Sum(item => item.When.GetDuration().TotalMinutes);
                    //compare to avarage load
                    FitnessLoad += Math.Sqrt(Math.Pow(load - averageLoad, 2.00));
                    //preferences
                    FitnessPref += entry.Key.CompareToPref(entry.Value)/100;
                }
                Fitness = FitnessLoad + FitnessPref;

            }
            return (double)Fitness;
        }

        public TimeSpan GetTravelTime(int location1, int location2)
        {
            TimeSpan TravelTime;
            //if (location1 < Loc.GetLength(0) && location2 < Loc.GetLength(1))
                TravelTime = new TimeSpan(0, Loc[location1, location2], 0);
            //else
              //  TravelTime = new TimeSpan(100000, 0, 0); //set to infinity
            return TravelTime;
        }

        public void SaveToFile(string filename)
        {
            string output = JsonConvert.SerializeObject(Assignment, Formatting.Indented);
            using (StreamWriter file = File.CreateText(filename + ".json"))
            {
                file.Write(output);
                //JsonSerializer serializer = new JsonSerializer();
                //serializer.Serialize(file, Assignment);
            }
        }
    }
}
