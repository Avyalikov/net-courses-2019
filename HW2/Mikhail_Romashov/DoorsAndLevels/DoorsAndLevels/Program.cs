using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutputComponent inputOutputComponent = new ConsoleInputOutput();

            DoorsAndLevels game = new DoorsAndLevels(inputOutputComponent);

            game.Run();
        }
    }
}
