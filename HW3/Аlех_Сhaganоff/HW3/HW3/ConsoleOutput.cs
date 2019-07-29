namespace HW3
{
    public class ConsoleOutput : ISendOutputProvider
    {
        public void PrintOutput(string text)
        {
            System.Console.WriteLine(text);
        }
    }
}