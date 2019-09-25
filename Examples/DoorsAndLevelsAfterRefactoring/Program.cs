using StructureMap;

namespace DoorsAndLevelsAfterRefactoring
{
    class Program
    {        
        static void Main()
        {
            var container = new Container(new DoorsAndLevelsRegistry());
            var game = container.GetInstance<IGame>();

            game.Run();
        }
    }
}
