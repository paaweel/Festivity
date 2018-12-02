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

        public EAAlgorithm(Festival festival, int populationSize = 10)
        {
            BestSolution = new Solution(festival);
            this.PopulationSize = populationSize;
            this.Festival = festival;
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

        public void CreateInitial()
        {
            foreach (var solution in Population)
            {
                solution.CreateRandom();
            }

        }
    }
}
