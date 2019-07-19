using System;

namespace Generics
{
    public class Sample
    {
        public void Test() {
            // avoid code duplication
            // provide type safety
            // no boxing/unboxing for example for List<int> (T is substituted during JIT-compilation)

            int[] arr = { 1, 5, 6, 7, 23 };
            string[] arr1 = { "1", "5", "sdsd", "dfwd" };
            double[] arr2 = { 45.6, 5.7, 3.6 };

            PrintArray(arr);
            PrintArray(arr1);
            PrintArray(arr2);

        }

        void PrintArray<T>(T[] arr)
        {
            foreach (var e in arr)
            {
                Console.WriteLine(e);
            }
        }

        void PrintArray(int[] arr)
        {
            foreach (var e in arr)
            {
                Console.WriteLine(e);
            }
        }

        void PrintArray(string[] arr)
        {
            foreach (var e in arr)
            {
                Console.WriteLine(e);
            }
        }

        void PrintArray(double[] arr)
        {
            foreach (var e in arr)
            {
                Console.WriteLine(e);
            }
        }
    }
}
