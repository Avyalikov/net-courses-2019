namespace DoorsAndLevelsRef
{
    public interface IInputOutput
    {
        /// <summary>Reds data and returns it.</summary>
        /// <returns></returns>
        string ReadInput();
        /// <summary>Reads and returns a char.</summary>
        /// <returns></returns>
        char ReadKey();
        /// <summary>Write data.</summary>
        /// <param name="dataToOutput">String to write.</param>
        void WriteOutput(string dataToOutput);
        /// <summary>Prints array of integers.</summary>
        /// <param name="array">Array to print.</param>
        void printArray(int[] array);
    }
}
