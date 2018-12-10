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
        public List<Solution> Population; //extract to another class?
        public Solution BestSolution;
        private static Random RandomGen = new Random();
        public IDataLoader DataLoader = new DataLoaderJson();

        public EAAlgorithm(int populationSize = 10)
        {
            Festival = DataLoader.LoadData("test");
            BestSolution = new Solution(Festival);
            PopulationSize = populationSize;
            Population = new List<Solution>();

            for (int i = 0; i < PopulationSize; ++i)
            {
                Population.Add(new Solution(Festival, RandomGen));
            }
        }


        public void Evolve()
        {
            var parent1 = Population[0];
            var parent2 = Population[1];


        }

        public void Mutate()
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
