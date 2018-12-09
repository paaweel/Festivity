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
        public IDataLoader dataLoader = new DataLoaderJson();

        public EAAlgorithm(Festival festival, int populationSize = 10)
        {
            BestSolution = new Solution(festival);
            PopulationSize = populationSize;
            Festival = festival;
            Population = new List<Solution>();
            
            for (int i = 0; i < PopulationSize; ++i)
            {
                Population.Add(new Solution(festival, RandomGen));
            }
           
        }

        public void Evolve()
        {
            
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
