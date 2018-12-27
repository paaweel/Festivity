using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class EAAlgorithm
    {
        private Festival Festival;
        private int PopulationSize;
        private int _ElitesNumber = 3;
        private int _CrossoverNumber = 3;
        public List<Solution> Population; //extract to another class?
        public List<Solution> NewPopulation;
        public Solution BestSolution;
        private static Random RandomGen = new Random();
        public IDataLoader DataLoader = new DataLoaderJson();

        public EAAlgorithm(int populationSize = 10)
        {
            Festival = DataLoader.LoadData("test");
            BestSolution = new Solution(Festival);
            PopulationSize = populationSize;
            Population = new List<Solution>(PopulationSize);
            NewPopulation = new List<Solution>(PopulationSize);

            for (int i = 0; i < PopulationSize; ++i)
            {
                Population.Add(new Solution(Festival, RandomGen));
            }
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
            Population = NewPopulation;
            NewPopulation.Clear();
        }

        public List<Solution> Elites()
        {
            var elites = new List<Solution>(_ElitesNumber);
            for (int i = 0; i < _ElitesNumber; i++)
            {
                elites.Add(Population[0]);
            }
            return elites;
        }

        public List<Solution> Selection()
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

        public List<Solution> Crossover()
        {
            var children = new List<Solution>(_CrossoverNumber);
            for (int i = 0; i < _CrossoverNumber; i++)
            {
                var parents = Selection();
                int pivot = RandomGen.Next(1, Festival.People.Count - 1);
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

        public void Mutate(Solution chromosome)
        {

        }

        public void Evaluate()
        {
            Population.Sort((x, y) => x.Evaluate().CompareTo(y.Evaluate())); //sort by fitness
            if (BestSolution.Evaluate() < Population[0].Evaluate())
            {
                BestSolution = Population[0];
                //event - bestSolution changed
            }
        }

        public void CreateInitial()
        {
            foreach (var solution in Population)
            {
                solution.CreateRandom();
            }

        }
    }
}
