namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;

    class ConsoleProvider : IInputAndOutput
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string Doors)
        {
            Console.WriteLine(Doors);
        }

        // a special method for the console provider
        public void Wait()
        {
            Console.ReadKey();
        }
    }
}
