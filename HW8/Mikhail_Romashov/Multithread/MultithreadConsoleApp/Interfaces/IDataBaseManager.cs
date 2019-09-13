using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Interfaces
{
    public interface IDataBaseManager
    {
        void AddLinksToDB(List<string> collection, int iteration);
    }
}
