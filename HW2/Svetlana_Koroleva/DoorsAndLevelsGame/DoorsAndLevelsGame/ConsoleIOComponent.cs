using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public class ConsoleIOComponent:IInputOutputComponent
    {
       public string ReadInput()
        {
            return Console.ReadLine();
        }

       public void WriteOutput(string data)
        {
            Console.WriteLine(data);
        }
    }
}
