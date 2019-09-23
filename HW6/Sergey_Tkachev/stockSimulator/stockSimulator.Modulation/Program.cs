namespace stockSimulator.Modulation
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            const int Period = 10000;
            const bool DbInitialize = true;

            Simutator simulator = new Simutator(Period, DbInitialize);

            simulator.Start();

            simulator.Stop();
        }
    }
}
