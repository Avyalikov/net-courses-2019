namespace DoorsAndLevelsRef
{
    internal interface IOperationWithData
    {
        /// <summary>Returns true if element contains in Array</summary>
        /// <param name="array">Array to check.</param>
        /// <param name="element">Element to find.</param>
        /// <returns></returns>
        bool Contains(int[] array, int element);

        void Divide(int[] array, int denominator);

        void Multiply(int[] array, int multiplier);
    }
}