using System;
using System.Collections.Generic;

namespace Generics
{
    public class Covariance
    {
        public static void Sample() {
            string s = "String";
            object o = s;

            // Can do?
            // List<string> ls = new List<string>();
            // List<object> lo = ls;

            //lo.Add("new string");
           //lo.Add(2);

            IEnumerable<String> strings = new List<String>() { "1", "2" };
            IEnumerable<Object> objects = strings;

            var enumerator = objects.GetEnumerator();
            enumerator.MoveNext();
            var test = enumerator.Current; // is in fact of type string
        }
    }
}
