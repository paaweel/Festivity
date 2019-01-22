using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    public class EAAlgorithm
    {
        private Festival Festival;
        private int PopulationSize;
        private int _ElitesNumber;
        private int _CrossoverNumber;
        private double _MutationPr;
        private List<Solution> Population; //extract to another class?
        private List<Solution> NewPopulation;
        private Solution BestSolution;
        private static Random RandomGen = new Random();
        private IDataLoader DataLoader = new DataLoaderJson();

        public List<double> BestSolLog = new List<double>();
        public List<double> PrefLog = new List<double>();
        public List<double> LoadLog = new List<double>();

        public int InvalidDueToShiftsOverlap = 0;
        public int InvalidDueToLocalization = 0;
        public int InvalidDueToWorkingOver6Day = 0;
        public int AllChecked = 0;

        public EAAlgorithm(int populationSize = 10, int elitesNumber = 2, int crossoverNumber = 5, double mutationPr= 0.2)
        {
            Festival = DataLoader.LoadData("test");
            BestSolution = new Solution(Festival);
            PopulationSize = populationSize;
            Population = new List<Solution>(PopulationSize);
            NewPopulation = new List<Solution>(PopulationSize);

            _ElitesNumber = elitesNumber;
            _CrossoverNumber = crossoverNumber;
            _MutationPr = mutationPr;

            for (int i = 0; i < PopulationSize; ++i)
            {
                Population.Add(new Solution(Festival, RandomGen));
            }
        }
       
        public List<double> GetBestSolutionFitness()
        {
            return BestSolLog;
        }

        public void Run()
        {
            Evaluate();
            NewPopulation.AddRange(Elites());
            NewPopulation.AddRange(Crossover());
            while (NewPopulation.Count < Population.Count)
            {
                NewPopulation.Add(new Solution(Festival, RandomGen));
            }
            
            foreach (var sol in NewPopulation)
            {
                if (RandomGen.NextDouble() <= _MutationPr)
                {
                    Mutate(sol);
                }
            }
            Population = NewPopulation;
            NewPopulation = new List<Solution>(PopulationSize);
        }

        private List<Solution> Elites()
        {
            var elites = new List<Solution>(_ElitesNumber);
            for (int i = 0; i < _ElitesNumber; i++)
            {
                elites.Add(Population[0]);
            }
            return elites;
        }

        private List<Solution> Selection()
        {
            double totalFitness = Population.Sum(item => item.Evaluate());
            List<Solution> Parents = new List<Solution>(2);

            do
            {
                double rnd1 = RandomGen.NextDouble() * totalFitness;
                double rnd2 = RandomGen.NextDouble() * totalFitness;
                Parents.Clear();
                foreach (var chromosome in Population)
                {
                    rnd1 -= chromosome.Evaluate();
                    if (rnd1 <= 0)
                    {
                        Parents.Add(chromosome);
                    }

                    rnd2 -= chromosome.Evaluate();
                    if (rnd2 <= 0)
                    {
                        if (!Parents.Contains(chromosome))
                        {
                            Parents.Add(chromosome);
                        }
                    }
                }
            } while (Parents.Count != 2);

            return Parents;
        }

        private List<Solution> Crossover()
        {
            var children = new List<Solution>(_CrossoverNumber);
            for (int i = 0; i < _CrossoverNumber; i++)
            {
                var parents = Selection();
                int pivot = (Festival.People.Count - 1) / 2; //RandomGen.Next(1, Festival.People.Count - 1);
                int counter = 0;
                var child = new Solution(Festival, RandomGen);
                foreach (var person in Festival.People)
                {
                    if (counter++ < pivot)
                    {
                        child.Assign(person, parents[0].Assignment[person]);
                    }
                    else
                    {
                        child.Assign(person, parents[1].Assignment[person]);
                    }
                }
                children.Add(child);
            }
            return children;
        }

        private void Mutate(Solution chromosome)
        {
            var person1 = Festival.People[RandomGen.Next(Festival.People.Count)];
            var person2 = Festival.People[RandomGen.Next(Festival.People.Count)];
            while (person1 == person2)
            {
                person1 = Festival.People[RandomGen.Next(Festival.People.Count)];
            }
            var temp = chromosome.Assignment[person1];
            chromosome.Assign(person1, chromosome.Assignment[person2], true);
            chromosome.Assign(person2, temp, true);
        }

        public void Evaluate()
        {
            Population.Sort((x, y) => x.Evaluate(true).CompareTo(y.Evaluate(true))); //sort by fitness

            if (BestSolution.Evaluate(true) > Population[0].Evaluate(true))
            {
                BestSolution = new Solution(Population[0]);
            }
            BestSolLog.Add(BestSolution.Evaluate(true));
            LoadLog.Add(BestSolution.FitnessLoad);
            PrefLog.Add(BestSolution.FitnessPref);
            InvalidDueToShiftsOverlap = Solution.InvalidDueToShiftsOverlap;
            InvalidDueToLocalization = Solution.InvalidDueToLocalization;
            InvalidDueToWorkingOver6Day = Solution.InvalidDueToWorkingOver6Day;
            AllChecked = Solution.AllChecked;
    }

        public void CreateInitial()
        {
            foreach (var solution in Population)
            {
                solution.CreateRandom();
            }
        }

        public void SaveBest(string filename)
        {
            BestSolution.SaveToFile(filename);
        }
    }
}
