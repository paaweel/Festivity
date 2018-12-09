using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festivity
{
    interface IDataLoader
    {
        Festival LoadData();
    }

    class DataLoaderConsole : IDataLoader
    {
        public Festival LoadData()
        {
            return new Festival();
        }
    }

    class DataLoaderJson : IDataLoader
    {
        public Festival LoadData()
        {
            return new Festival();
        }
    }

    class DataLoaderApp : IDataLoader
    {
        public Festival LoadData()
        {
            return new Festival();
        }
    }
}
