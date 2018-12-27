using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Solution
    {
        private static List<Person> Ppl;
        private static List<Shift> Shifts;
        public Dictionary<Person, List<Shift>> Assignment;
        private double? Fitness;
        static private Random RandomGen = new Random();

        public Solution(Festival festival, Random randomGen = null)
        {
            Ppl = festival.People;
            Shifts = festival.Shifts;
            Assignment = new Dictionary<Person, List<Shift>>();
            if (randomGen != null) RandomGen = randomGen;
            else RandomGen = new Random();
            Fitness = null;

            CreateRandom();
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
            Assignment[person].Add(shift);
            return true;
        }

        public bool Assign(Person person, List<Shift> shifts)
        {
            bool ret = true;
            foreach (var shift in shifts)
            {
                if (Assign(person, shift) != true)
                {
                    ret = false;
                }
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
            } while (!this.Validate());
            this.Evaluate();
        }
       
        public bool Validate()
        {
            bool validity = true;
            /*
            TimeSpan counter = new TimeSpan(0, 0, 0);
            int howManyPpl = Ppl.Count;
            int sizeOfList = Shifts.Count;
            int[] shiftDuration = new int[sizeOfList];
            TimeSpan minHoursofWork = new TimeSpan(30, 0, 0);
            TimeSpan maxShiftDuration = new TimeSpan(3, 0, 0);

            while (howManyPpl >= 0)
            {
                if (Ppl[howManyPpl].Availability.GetDuration() < minHoursofWork)
                {
                    Console.WriteLine("Not enough hours of work.");
                    validity = false;
                    break;
                }

                while (sizeOfList >= 0)
                {
                    if ((Shifts[sizeOfList].PplNeeded - Shifts[sizeOfList].PplAssigned) != 0)
                    {
                        if (Ppl.All.Availability[Shifts[sizeOfList].When] != (0, 0, 0))
                        {
                            Console.WriteLine("There are still some people who can be assigned to this shift.");
                            validity = false;
                            break;
                            //no to nie jest do konca zrobione, bo nie ma metody Assign i nie można sprawdzić
                            //czy ktos juz dostał te zmiane
                        }
                        Console.WriteLine("There is more people needed for this shift.");
                        //  if(Ppl[howManyPpl].Availability[Shifts[sizeOfList].When() != (0,0,0)])
                        // Console.WriteLine("This person can be assigned to this shift.");
                        validity = false;
                        break;
                    }

                    if (Ppl.All.Availability[Shifts[sizeOfList].When] == (0, 0, 0))
                    {
                        Console.WriteLine("Not enough people");
                        validity = false;
                        break;
                        //tutaj przy okazji trzeba jakos przypisac zmiany wolontariuszom, żeby możn też było sprawdzić
                        //czy jest wystarczajaca ilosc z dyspozycyjnoscia na konkretna zmiane
                    }

                    //elko brakuje mi tu metody Assign, jak będzie, to sprawdzę jeszcze czy nie za długa
                    //jest jedna zmiana

                    if (Shifts[sizeOfList].When.GetDuration() > maxShiftDuration)
                        Console.WriteLine("Shift duration is too long");
                    //tutaj nalezy dodac funkcjonalnosc sprawdzania czy wolontariusz nie ma kilku zmian pod rząd
                    //które przekraczają maksymalny czas jednej zmiany
                    sizeOfList--;
                }
                howManyPpl--;
            }
            */
            return validity;
        }

        public double Evaluate(bool eval = false)
        {
            if (Fitness == null || eval)
            {
                // calculate Fitness
                Fitness = 0;

                // 1 - calculate avarege load
                int nOfPeople = Assignment.Count;
                int totalLoad = 0;
                foreach (KeyValuePair<Person, List<Shift>> entry in Assignment)
                {
                    totalLoad += entry.Value.Sum(item => item.When.GetDuration().Minutes);
                }
                double averageLoad = totalLoad / nOfPeople;

                foreach (KeyValuePair<Person, List<Shift>> entry in Assignment)
                {
                    int load = entry.Value.Sum(item => item.When.GetDuration().Minutes);
                    foreach (var shift in entry.Value)
                    {
                        Fitness += Math.Sqrt(Math.Pow(load - averageLoad, 2.00));
                    }
                }

                // 2 - preferences??
                // 3 - ???
            }
            return (double)Fitness;
        }
    }
}
