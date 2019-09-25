
using MyType = System.Int32;

namespace HW2
{
    public interface IStorageProvider
    {
        MyType peek();
        void push(MyType value);
        MyType pop();
        int Count { get; }
    }
}