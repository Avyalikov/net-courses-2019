using Doors_and_levels_game.Interfaces;
using System;
using System.Collections.Generic;

namespace Doors_and_levels_game.Components
{
    class StorageModule : IStorageModule<ulong, List<ulong>>
    {
        public List<ulong> Doors { get; set; }
        Stack<ulong> chosenDoors = new Stack<ulong>();

        public ulong PopChosenDoor()
        {
            if (chosenDoors.TryPop(out ulong door))
                return door;

            throw new Exception("Stack is empty");
        }
        public void PushChosenDoor(ulong door) => chosenDoors.Push(door);
        public bool ChosenDoorIsEmpty() => chosenDoors.Count == 0;
    }
}