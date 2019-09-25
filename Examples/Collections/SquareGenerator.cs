using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class SquareGenerator : IEnumerable<int>
    {
        int steps;

        public SquareGenerator(int steps)
        {
            this.steps = steps;
        }

        public IEnumerator GetEnumerator()
        {
            return new Cursor(steps);
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            return new Cursor(steps);
        }

        internal class Cursor : IEnumerator<int>
        {
            private int maxSteps;
            private int step = 0;

            internal Cursor(int maxSteps)
            {
                this.maxSteps = maxSteps;
            }

            #region IEnumerator<int>
            public int Current
            {
                get
                {
                    return step * step;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                step = 0;
            }

            public bool MoveNext()
            {
                if (step < maxSteps)
                {
                    step++; return true;
                }
                return false;
            }

            #endregion
        }
    }

    public static class YieldSquareGenerator
    {
        public static IEnumerable<int> Generate(int steps)
        {
            for (int i = 1; i <= steps; i++)
            {
                yield return i * i;
            }
        }
    }
}
