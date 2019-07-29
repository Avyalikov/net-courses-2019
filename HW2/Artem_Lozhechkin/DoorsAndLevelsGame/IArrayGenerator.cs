namespace DoorsAndLevelsGame
{
    /// <summary>
    /// This interface identifies a class generates an array of special data.
    /// </summary>
    /// <typeparam name="T">Data type which should be in array.</typeparam>
    interface IArrayGenerator<T>
    {
        /// <summary>
        /// Returns an array of special data.
        /// </summary>
        /// <returns></returns>
        T[] GetArray(int size);
    }
}
