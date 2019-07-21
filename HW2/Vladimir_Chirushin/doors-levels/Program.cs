using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_levels
{
    partial class Program
    {
        static void Main(string[] args)
        {
            IInputOutputDevice inputOutputDevice = new ConsoleIODevice();
            DoorsGame doorsGame = new DoorsGame(inputOutputDevice: inputOutputDevice);
            doorsGame.Run();
        }
    }
}
