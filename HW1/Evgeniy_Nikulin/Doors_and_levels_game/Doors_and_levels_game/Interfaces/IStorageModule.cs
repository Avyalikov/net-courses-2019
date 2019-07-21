using System.Collections;

namespace Doors_and_levels_game.Interfaces
{
    public interface IStorageModule<T, Tlist> where Tlist : IList
    {
        Tlist Doors { get; set; }

        T PopChosenDoor();
        void PushChosenDoor(T door);
        bool ChosenDoorIsEmpty();
        
    }
}