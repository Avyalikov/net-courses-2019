namespace trading_software
{
    using System.Timers;

    public class TimeManager : ITimeManager
    {
        private readonly ITransactionManager transactionManager;
        private readonly IOutputDevice outputDevice;

        private Timer aTimer;
        private double timeInterval= 100;
        private int addedTransactionCount;

        public TimeManager(
            IOutputDevice outputDevice,
            ITransactionManager transactionManager
            )
        {
            this.outputDevice = outputDevice;
            this.transactionManager = transactionManager;
        }

        private void SetTimer(double time)
        {
            aTimer = new Timer(time);
            aTimer.Elapsed += actionOnTime;
            aTimer.AutoReset = true;
        }

        private void StartTimer()
        {
            if (aTimer != null && !aTimer.Enabled)
            {
                addedTransactionCount = 0;
                aTimer.Enabled = true;
            }
        }

        private void StopTimer()
        {
            if (aTimer.Enabled)
            {
                aTimer.Enabled = false;
            }
        }

        private void actionOnTime(object sender, ElapsedEventArgs e)
        {
            if (transactionManager.MakeRandomTransaction())
            {
                addedTransactionCount++;
            }
        }

        private void TransactionsCreatedOverTime()
        {
            outputDevice.WriteLine($"There was {addedTransactionCount} transactions added!");
        }

        public void StartRandomTransactionThread()
        {
            SetTimer(timeInterval);
            StartTimer(); 
        }

        public void StopRandomTransactionThread()
        {
            StopTimer();
            TransactionsCreatedOverTime();
        }
    }
}
