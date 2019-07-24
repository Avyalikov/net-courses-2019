namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;

    class ConsoleProvider : IInputAndOutput
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteDoors(string Doors)
        {
            Console.WriteLine(Doors);
        }

        public char ReadKeyForExit()
        {
            return Console.ReadKey().KeyChar;
        }
    }
}
