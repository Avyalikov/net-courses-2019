using System;

namespace HW2_doors_and_levels_refactoring
{
    public class ConsoleIODevice : IInputOutputDevice
    {
        public string InputValue()
        {
            return Console.ReadLine();
        }

        public void Print(string PrintValue)
        {
            Console.WriteLine(PrintValue);
        }

        public char KeyInput()
        {
            return Console.ReadKey().KeyChar;
        }
    }

}