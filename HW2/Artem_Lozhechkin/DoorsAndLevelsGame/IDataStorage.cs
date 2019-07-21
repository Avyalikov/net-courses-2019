namespace DoorsAndLevelsGame
{
    /// <summary>
    /// IStackDataStorage should emulate stack data structure.
    /// <typeparamref name="T"/>Element of data in DataStorage</typeparam>
    /// </summary>
    interface IStackDataStorage<T>
    {
        /// <summary>
        /// Inserts an object at the top of the StackDataStorage.
        /// </summary>
        /// <param name="data">This object pushes onto the StackDataStorage.</param>
        void Push(T data);
        /// <summary>
        /// Peeks data at the top of the StackDataStorage without removing it.
        /// </summary>
        /// <returns>An object at the top of the StackDataStorage.</returns>
        T Peek();
        /// <summary>
        /// Returns an object from the top of the StackDataStorage and removes it.
        /// </summary>
        /// <returns>An object at the top of the StackDataStorage.</returns>
        T Pop();
        /// <summary>
        /// Returns the size of StackDataStorage.
        /// </summary>
        /// <returns>Size of the StackDataStorage.</returns>
        int GetSize();
        /// <summary>
        /// Checks whether the data contains in a StackDataStorage or not.
        /// </summary>
        /// <param name="data">Data which can be contained in StackDataStorage.</param>
        /// <returns>True if Data is in StackDataStorage and False if Data is not in StackDataStorage.</returns>
        bool Contains(T data);
        /// <summary>
        /// Removes all elements from StackDataStorage.
        /// </summary>
        void Clear();
    }
}
