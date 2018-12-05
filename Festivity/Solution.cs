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
        static private Random RandomGen = new Random();
        private bool RandomGenSet = false;

        public Solution(Festival festival, Random randomGen) : this(festival)
        {
            //this.RandomGen = randomGen;
            this.RandomGenSet = true;
        }

        public Solution(Festival festival)
        {
            this.Ppl = festival.People;
            this.Shifts = festival.Shifts;
            this.Assignment = new Dictionary<Person, List<Shift>>();
            if (!RandomGenSet)
            {
                //this.RandomGen = new Random();
            }
            CreateRandom();
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
                    //generate random indexes for ppl and shifts
                    pplIndex = RandomGen.Next(Ppl.Count);

                    do
                    {
                        shiftsIndex = RandomGen.Next(Shifts.Count);
                    } while (usedShifts.Contains(shiftsIndex) && usedShifts.Count < Shifts.Count);

                    //Shifts[shiftsIndex].PplAssigned++;

                    usedShifts.Add(shiftsIndex);

                    if (!Assignment.ContainsKey(Ppl[pplIndex]))
                    {
                        Assignment.Add(Ppl[pplIndex], new List<Shift>());
                    }
                    Assignment[Ppl[pplIndex]].Add(Shifts[shiftsIndex]);
                    
                }
            } while (!this.Validate());
        }

 public bool Validate(Festival festival)
    {
        TimeSpan counter = new TimeSpan(0,0,0);
        int howManyPpl = Ppl.Count;
        int sizeOfList = Shifts.Count;
        int[] shiftDuration = new int [sizeOfList];
        TimeSpan minHoursofWork = new TimeSpan(30,0,0);
        TimeSpan maxShiftDuration = new TimeSpan(3,0,0);
             
         while (howManyPpl >= 0)
             {  
                    if(Ppl[howManyPpl].Availability.GetDuration() < minHoursofWork)
                    {
                        Console.WriteLine("Not enough hours of work.");
                        return false;
                    }

                     while (sizeOfList >= 0)
                     {
                        if((Shifts[sizeOfList].PplNeeded - Shifts[sizeOfList].PplAssigned) != 0)
                        {
                            if (Ppl.All.Availability[Shifts[sizeOfList].When] != (0,0,0))
                            {
                                Console.WriteLine("There are still some people who can be assigned to this shift.");
                                return false;
                                  //no to nie jest do konca zrobione, bo nie ma metody Assign i nie można sprawdzić
                                  //czy ktos juz dostał te zmiane
                            }

                            Console.WriteLine("There is more people needed for this shift.");
                         //  if(Ppl[howManyPpl].Availability[Shifts[sizeOfList].When() != (0,0,0)])
                           // Console.WriteLine("This person can be assigned to this shift.");
                           return false;
                        }

                        if (Ppl.All.Availability[Shifts[sizeOfList].When] == (0,0,0))
                        {
                            Console.WriteLine("Not enough people");
                            return false;
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
