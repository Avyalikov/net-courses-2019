namespace TradingApp.View.Interface
{
    interface IIOProvider
    {
        void WriteLine(string line);

        string ReadLine();

        void ReadKey();

        void Clear();
    }
}
