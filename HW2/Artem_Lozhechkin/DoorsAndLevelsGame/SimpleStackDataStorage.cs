using System.Collections.Generic;

namespace DoorsAndLevelsGame
{
    /// <summary>
    /// SimpleStackDataStorage<T> is an IStackDataStorage<T> implementation which contains simple Stack in it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class SimpleStackDataStorage<T> : IStackDataStorage<T>
    {
        private Stack<T> DataStorage { get; set; } = new Stack<T>();
        /// <summary>
        /// Inserts an object at the top of the StackDataStorage.
        /// </summary>
        /// <param name="data">This object pushes onto the StackDataStorage.</param>
        void IStackDataStorage<T>.Push(T data) => DataStorage.Push(data);
        /// <summary>
        /// Peeks data at the top of the StackDataStorage without removing it.
        /// </summary>
        /// <returns>An object at the top of the StackDataStorage.</returns>
        T IStackDataStorage<T>.Peek() => DataStorage.Peek();
        /// <summary>
        /// Returns an object from the top of the StackDataStorage and removes it.
        /// </summary>
        /// <returns>An object at the top of the StackDataStorage.</returns>
        T IStackDataStorage<T>.Pop() => DataStorage.Pop();
        /// <summary>
        /// Returns the size of StackDataStorage.
        /// </summary>
        /// <returns>Size of the StackDataStorage.</returns>
        int IStackDataStorage<T>.GetSize() => DataStorage.Count;
        /// <summary>
        /// Checks whether the data contains in a StackDataStorage or not.
        /// </summary>
        /// <param name="data">Data which can be contained in StackDataStorage.</param>
        /// <returns>True if Data is in StackDataStorage and False if Data is not in StackDataStorage.</returns>
        bool IStackDataStorage<T>.Contains(T data) => DataStorage.Contains(data);
        /// <summary>
        /// Removes all elements from StackDataStorage.
        /// </summary>
        void IStackDataStorage<T>.Clear() => DataStorage.Clear();
    }
}
