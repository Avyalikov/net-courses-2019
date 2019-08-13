namespace Trading.Interface
{
    public interface IIOProvider
    {
        void WriteLine(string line);

        string ReadLine();

        void ReadKey();

        void Clear();
    }
}
