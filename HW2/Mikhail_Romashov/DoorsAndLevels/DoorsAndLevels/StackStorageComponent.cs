using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    public class StackStorageComponent : IStorageComponent
    {
        private Stack<int> stack;

        public StackStorageComponent()
        {
            stack = new Stack<int>();
        }
        public int GetSize()
        {
            return stack.Count;
        }
        public int Pop()
        {
            return stack.Pop();
        }
        public void Push(int value)
        {
            stack.Push(value);
        }
        public void Clear()
        {
            stack.Clear();
        }
    }
}
