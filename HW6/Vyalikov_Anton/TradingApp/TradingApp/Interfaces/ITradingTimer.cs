namespace TradingApp.Interfaces
{
    using System.Timers;
    interface ITradingTimer
    {
        void StartRandomTrading();
        void StopRandomTrading();
    }
}
