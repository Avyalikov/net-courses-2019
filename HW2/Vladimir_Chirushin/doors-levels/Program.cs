using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_levels
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputOutputDevice inputOutputDevice = new ConsoleIODevice();
            IDoorsGenerator doorsGenerator = new DoorsGenerator();
            DoorsGame doorsGame = new DoorsGame(inputOutputDevice, doorsGenerator);
            doorsGame.Run();
        }
    }
}
