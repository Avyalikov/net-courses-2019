namespace DoorsAndLevelsRefactoring.Provider
{
    using DoorsAndLevelsRefactoring.Interface;
    using System;
    using System.Windows.Forms;

    class ConsoleWithMessageBoxProvider : IInputAndOutput
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string Doors)
        {
            MessageBox.Show(Doors);
        }

        // a special method for the console provider
        public void Wait()
        {
            Console.ReadKey();
        }
    }
}
