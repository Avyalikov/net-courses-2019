namespace DoorsAndLevelsRef
{
    internal interface IOperationWithData
    {
        bool Contains(int[] array, int element);

        void Divide(int[] array, int denominator);

        void Multiply(int[] array, int multiplier);
    }
}