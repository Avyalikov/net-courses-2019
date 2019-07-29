
using MyType = System.Int32;

namespace HW2
{
    public interface IRandomProvider
    {
        MyType[] rand(Settings settings);
    }
}