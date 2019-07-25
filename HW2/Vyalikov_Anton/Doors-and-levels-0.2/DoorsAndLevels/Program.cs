namespace DoorsAndLevels
{
    class Program
    {
        static void Main(string[] args)
        {
            Interfaces.IDoorsGenerator doorsGenerator = new DoorsNumGenerator();
            Game game = new Game(doorsGenerator);
            game.Start(5);
        }
    }
}
