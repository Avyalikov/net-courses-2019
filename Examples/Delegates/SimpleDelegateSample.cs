using System;

namespace Delegates
{
    delegate int Transform(int x);

    public class SimpleDelegateSample
    {
        static int Square(int x)
        {
            return x * x;
        }

        static int Add(int x)
        {
            return x + x;
        }

        static int DoubleTransform(int x, Transform trans)
        {
            return trans(trans(x));
        }

        public void TransformTest()
        {
            Transform t = Square;                 // Create delegate instance
            var result = t(3);                      // Invoke delegate

            t = Add;
            var test = t(3);


            var result2 = t.Invoke(5);              // Invoke delegate

            var result3 = DoubleTransform(6, Add);  // Delegate as parameter

            var result4 = DoubleTransform(3, Square);

            Console.WriteLine("{0} {1} {2}", result, result2, result3);

        }
    }
}
