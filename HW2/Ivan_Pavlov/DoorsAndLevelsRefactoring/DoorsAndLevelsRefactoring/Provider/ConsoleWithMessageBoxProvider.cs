namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;
    using System.Windows.Forms;

    class ConsoleWithMessageBoxProvider : IInputAndOutput
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void WriteOutput(string Doors)
        {
            MessageBox.Show(Doors);
        }

        public char ReadKeyForExit()
        {
            return Console.ReadKey().KeyChar;
        }
    }
}
