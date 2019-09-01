namespace TradingSimulator.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int n)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (n < 0)
            {
                throw new ArgumentOutOfRangeException("n", "n must be 0 or greater");
            }
            int j = collection.Count();
            if (j <= n)
            {
                return collection;
            }

            List<T> temp = new List<T>();

            for (int i = n; i >= 1; i--)
            {
                temp.Add(collection.ElementAt(j - i));
            }

            return temp;
        }
    }
}