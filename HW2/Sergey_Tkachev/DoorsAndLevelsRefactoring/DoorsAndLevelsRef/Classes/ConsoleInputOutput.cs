using System;

namespace DoorsAndLevelsRef
{
    internal class ConsoleInputOutput : IInputOutput
    {
        /// <summary>Checks if entered number is integer, if not then number should be entered again.</summary>
        /// <returns></returns>
        public string ReadInput()
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int enteredNum))
                    Console.Write("Incorrect input. Please try again: ");
                else return enteredNum.ToString();
        }
        /// <summary>Returns a char from Console input.</summary>
        /// <returns></returns>
        public char ReadKey()
        {
            return Console.ReadKey().KeyChar;
        }
        /// <summary>Prints data in Console.</summary>
        /// <param name="dataToOutput">String to print.</param>
        public void WriteOutput(string dataToOutput)
        {
            Console.Write(dataToOutput);
        }

        /// <summary>Prints array into console.</summary>
        /// <param name="array">Array of integers to print</param>
        public void printArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.Write(".");
        }
    }
}