namespace DoorsAndLevelsRef
{ 
    public interface IArrayGenerator
    {
        /// <summary>Generates array of integers.</summary>
        /// <param name="elementsAmount">Amount of elements in array.</param>
        /// <returns></returns>
        int[] GenerateArray(int elementsAmount);
    }
}
