namespace TradingSoftware.ConsoleClient.Services
{
    using System.Timers;
    using TradingSoftware.ConsoleClient.Devices;

    public class TimeManager : ITimeManager
    {
        private readonly ISimulationManager simulationManager;
        private readonly IOutputDevice outputDevice;

        private Timer simulationTimer;
        private double timeInterval = 100;
        private int addedTransactionCount;

        public TimeManager(
            IOutputDevice outputDevice,
            ISimulationManager simulationManager)
        {
            this.outputDevice = outputDevice;
            this.simulationManager = simulationManager;
        }

        public void StartRandomTransactionThread()
        {
            this.SetTimer(this.timeInterval);
            this.StartTimer();
        }

        public void StopRandomTransactionThread()
        {
            this.StopTimer();
            this.TransactionsCreatedOverTime();
        }

        private void SetTimer(double time)
        {
            this.simulationTimer = new Timer(time);
            this.simulationTimer.Elapsed += this.ActionOnTime;
            this.simulationTimer.AutoReset = true;
        }

        private void StartTimer()
        {
            if (this.simulationTimer != null && !this.simulationTimer.Enabled)
            {
                this.addedTransactionCount = 0;
                this.simulationTimer.Enabled = true;
            }
        }

        private void StopTimer()
        {
            if (this.simulationTimer != null && this.simulationTimer.Enabled)
            {
                this.simulationTimer.Enabled = false;
            }
        }

        private void ActionOnTime(object sender, ElapsedEventArgs e)
        {
            if (this.simulationManager.MakeRandomTransaction())
            {
                this.addedTransactionCount++;
            }
        }

        private void TransactionsCreatedOverTime()
        {
            this.outputDevice.WriteLine($"There was {addedTransactionCount} transactions added!");
        }
    }
}