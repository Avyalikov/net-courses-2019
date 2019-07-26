namespace EventsExample
{
    using System;
    using System.Threading.Tasks;

    public class Worker
    {
        public event EventHandler<Worker> OnProgress;

        public delegate void ProgressDelegate(int percentComplete);
        public event ProgressDelegate Progress;
        public void DoWork()
        {
            for (int i = 0; i < 10; i++)
            {
                Progress(i * 10);

                OnProgress(this, this);
                System.Threading.Thread.Sleep(100);
            }
        }
    }

    public class ClientA
    {
        private readonly Worker worker;

        public ClientA(Worker worker)
        {
            this.worker = worker;
        }

        public void Subscribe()
        {
            this.worker.Progress += this.worker_Progress;

            this.worker.OnProgress += this.Worker_OnProgress;
        }

        private void Worker_OnProgress(object sender, Worker e)
        {
            
        }

        protected virtual void worker_Progress(int percentComplete)
        {
            Console.WriteLine($"ClientA: {percentComplete}");
        }
    }

    public class ClientB : ClientA
    {
        public ClientB(Worker worker): base(worker)
        {

        }
        protected override void Worker_Progress(int percentComplete)
        {
            Console.WriteLine($"ClientB: {percentComplete}");
        }
    }

    public class ClientC
    {
        public void worker_Progress(int percentComplete)
        {
            Task.Run(delegate
            {
                try
                {
                    Console.WriteLine($"ClientC: {percentComplete}");

                    throw new Exception();
                }
                catch ( Exception ex)
                {

                }
            });
        }
    }
     
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var worker = new Worker();
            var clientA = new ClientA(worker);
            var clientB = new ClientB(worker);

            Task.Run(() =>
            {
                var clientC = new ClientC();
                worker.Progress += clientC.worker_Progress;
            });
            
            clientA.Subscribe();
            clientB.Subscribe();
             
            worker.DoWork();
        }
    }
}
