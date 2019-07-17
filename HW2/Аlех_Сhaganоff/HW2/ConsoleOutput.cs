using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    public class ConsoleOutput : ISendOutputProvider
    {
        public void sendOutput(MyType[] currentNumbers)
        {
            foreach (MyType n in currentNumbers)
            {
                Console.Write(n + " ");
            }
            Console.WriteLine();
        }

        public void printOutput(string text)
        {
            Console.WriteLine(text);
        }
    }
}