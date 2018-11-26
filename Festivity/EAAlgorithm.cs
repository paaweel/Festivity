using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class EAAlgorithm
    {

        public EAAlgorithm(Festival festival, int populationSize = 10)
        {
            this.PopulationSize = populationSize;
            this.Festival = festival;
        }
        
        private Festival Festival;
        private int PopulationSize;
        public List<Solution> Population; //extract to another class?
        public Solution BestSolution;

        public void Evolve()
        {

        }
        public void Mutate()
        {

        }

        public void CreateInitial()
        {
 

        }
    }
}
