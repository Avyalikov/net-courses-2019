namespace TradingSoftware.ConsoleClient.Services
{
    public interface ITimeManager
    {
        void StartRandomTransactionThread();

        void StopRandomTransactionThread();
    }
}