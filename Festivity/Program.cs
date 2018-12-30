using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    class Program
    {

        static void Main(string[] args)
        {
            
            DataCreator d = new DataCreator();
            d.CreateAndSaveDataToFile("test", 20, 40);

            EAAlgorithm alg = new EAAlgorithm(100);
            
            for (int i = 0; i < 100; ++i)
            {
                alg.Run();
                Console.WriteLine(alg.BestSolution.Evaluate());
            }

            Console.ReadKey();
        }

    }
}
