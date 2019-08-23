using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.ConsoleApp
{
    public interface IIOProvider
    {
        void WriteLine(string message);
        string ReadLine();
    }
}
