namespace TradingApp.View
{
    using TradingApp.Data;
    using TradingApp.View.Provider;
    using TradingApp.View.View;

    class Program
    {
        static void Main(string[] args)
        {
            MainPage mp = new MainPage(
                logic: new TradeLogic(new AppDbContext()),
                iOProvider: new ConsoleIO(),
                phraseProvider: new JsonPhraseProvider());

            while (true)
                mp.Run();
        }
    }
}
