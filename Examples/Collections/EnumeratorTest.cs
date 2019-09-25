using System;
using System.Collections.Generic;

namespace Collections
{
    public class EnumeratorTest
    {
        public void SquareGeneratorTest()
        {
            foreach (var sq in new SquareGenerator(5))
            {
                Console.WriteLine(sq);
            }
        }

        public void YieldSquareGeneratorTest()
        {
            foreach (var sq in YieldSquareGenerator.Generate(5))
            {
                Console.WriteLine(sq);
            }
        }

        public void ForeachSample()
        {
            List<int> a = new List<int> { 1, 2, 3, 4 };

            foreach (var ai in a)
            {
                Console.WriteLine(ai);
            }

            var enumerator = a.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var ai = enumerator.Current;
                Console.WriteLine(ai);
            }
        }
    }
}
