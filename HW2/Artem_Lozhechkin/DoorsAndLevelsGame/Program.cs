namespace DoorsAndLevelsGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new SimpleRandomLongArrayGenerator(), new SimpleStackDataStorage<int>());
            game.Play();
        }
    }
}
