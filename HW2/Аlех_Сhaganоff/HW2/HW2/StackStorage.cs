using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    public class StackStorage : IStorageProvider
    {
        private Stack<MyType> numbers = new Stack<MyType>();

        public int Count => numbers.Count;

        public MyType peek()
        {
            return numbers.Peek();
        }

        public MyType pop()
        {
            return numbers.Pop();
        }

        public void push(MyType value)
        {
            numbers.Push(value);
        }
    }
}
   

