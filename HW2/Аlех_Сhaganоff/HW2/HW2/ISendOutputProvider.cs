
using MyType = System.Int32;

namespace HW2
{
    public interface ISendOutputProvider
    {
        void sendOutput(MyType[] currentNumbers);
        void printOutput(string text);
    }
}