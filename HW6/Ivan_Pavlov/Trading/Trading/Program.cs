namespace Trading
{
    using Trading.Logic;
    using TradingData;
    using TradingView;

    class Program
    {
        static void Main(string[] args)
        {
            Home home = new Home(new ViewProvider(), new DbProvider());

            while (true)
            {
                home.Run();
            }
        }
    }
}
