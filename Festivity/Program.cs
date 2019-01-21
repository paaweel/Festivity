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
            d.CreateAndSaveDataToFile("test", 4, 8);

            EAAlgorithm alg = new EAAlgorithm(7, 1, 3, 0.6);
            
            for (int i = 0; i < 100; ++i)
            {
                alg.Run();
                Console.WriteLine(alg.BestSolLog.Last());
            }

            Console.ReadKey();
        }

    }
}
