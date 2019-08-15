namespace TradingView.Interface
{
    internal interface IIOProvider
    {
        void WriteLine(string line);

        string ReadLine();

        void ReadKey();

        void Clear();
    }
}
