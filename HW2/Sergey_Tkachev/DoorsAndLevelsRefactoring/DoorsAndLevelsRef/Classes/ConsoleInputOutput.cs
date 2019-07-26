using System;

namespace DoorsAndLevelsRef
{
    internal class ConsoleInputOutput : IInputOutput
    {
        private readonly JsonPhraseProvider jsonPhraseProvider;

        public ConsoleInputOutput(IPhraseProvider phraseProvider)
        {
            this.jsonPhraseProvider = (JsonPhraseProvider) phraseProvider;
        }

        /// <summary>Returns string from console</summary>
        /// <returns></returns>
        public string ReadInput()
        {
            return Console.ReadLine();
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
            Console.WriteLine(".");
        }
    }
}