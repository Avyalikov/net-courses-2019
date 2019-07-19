namespace DoorsAndLevelsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new SimpleRandomLongArrayGenerator());
            game.Play();
        }
    }
}
