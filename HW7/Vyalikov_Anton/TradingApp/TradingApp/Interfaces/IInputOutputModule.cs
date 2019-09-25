namespace TradingApp.Interfaces
{
    interface IInputOutputModule
    {
        string ReadInput();
        void WriteOutput(string outputData);
        void Clear();
    }
}
