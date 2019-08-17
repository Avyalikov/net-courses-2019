namespace trading_software
{
    public interface ITimeManager
    {
        void StartRandomTransactionThread();
        void StopRandomTransactionThread();
    }
}