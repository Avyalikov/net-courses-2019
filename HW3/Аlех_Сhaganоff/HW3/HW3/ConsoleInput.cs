namespace HW3
{
    public class ConsoleInput : IReadInputProvider
    {
        public string ReadInput()
        {
            return System.Console.ReadLine();
        }
    }
}