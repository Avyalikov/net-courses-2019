using System.Collections;

namespace Doors_and_levels_game.Interfaces
{
    public interface IDoorsGenerater<T> where T : IList
    {
        T Generate(int n);
    }
}