namespace DoorsAndLevelsRef
{
    internal class OperationWithArrays : IOperationWithData
    {
        public OperationWithArrays()
        {
        }
        /// <summary>Checks if array contains a number as an element.</summary>
        /// <param name="array">Array to check</param>
        /// <param name="element">Number to find</param>
        /// <returns></returns>
        public bool Contains(int[] array, int element)
        {
            for (int i = 0; i < array.Length; i++)
                if (element == array[i])
                    return true;
            return false;
        }
        /// <summary>Divides all elements of array to a number.</summary>
        /// <param name="array">Array of integers</param>
        /// <param name="denominator">Denominator</param>
        public void Divide(int[] array, int denominator)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] /= denominator;
            }
        }
        /// <summary>Multiplies all elements of array to a number.</summary>
        /// <param name="array">Array of integers</param>
        /// <param name="multiplier">Multiplier</param>
        public void Multiply(int[] array, int multiplier)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] *= multiplier;
            }
        }
    }
}