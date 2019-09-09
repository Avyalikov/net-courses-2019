namespace TradingApp.Services
{
    using System.Timers;
    using Interfaces;

    class TradingTimer : ITradingTimer
    {
        private readonly ITradingSimulation tradingSimulation;
        private readonly IInputOutputModule ioModule;

        private Timer timer;
        private double timeInterval = 100;
        private int transactionCounter;

        public TradingTimer(
            ITradingSimulation tradingSimulation,
            IInputOutputModule ioModule)
        {
            this.tradingSimulation = tradingSimulation;
            this.ioModule = ioModule;
        }

        public void StartRandomTrading()
        {
            this.SetTimer(this.timeInterval);
            this.StartTimer();
        }

        public void StopRandomTrading()
        {
            this.StopTimer();
            this.TransactionsCreatedOverTime();
        }

        private void SetTimer(double time)
        {
            this.timer = new Timer(time);
            this.timer.Elapsed += this.ActionOnTime;
            this.timer.AutoReset = true;
        }

        private void StartTimer()
        {
            if (this.timer != null && !this.timer.Enabled)
            {
                this.transactionCounter = 0;
                this.timer.Enabled = true;
            }
        }

        private void StopTimer()
        {
            if (this.timer != null && this.timer.Enabled)
            {
                this.timer.Enabled = false;
            }
        }

        private void ActionOnTime(object sender, ElapsedEventArgs e)
        {
            if (this.tradingSimulation.Trading())
            {
                this.transactionCounter++;
            }
        }

        private void TransactionsCreatedOverTime()
        {
            this.ioModule.WriteOutput($"There was {transactionCounter} transactions added!");
        }
    }
}
