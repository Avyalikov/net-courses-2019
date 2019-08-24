namespace TradingApp.View
{
    using StructureMap;
    using TradingApp.View.DependencyInjection;
    using TradingApp.View.Provider;
    using TradingApp.View.View;

    class Program
    {
        static void Main(string[] args)
        {
            MainPage mp = new MainPage(
                container: new Container(new TradeRegistry()),
                iOProvider: new ConsoleIO(),
                phraseProvider: new JsonPhraseProvider());

            while (true)
                mp.Run();
        }
    }
}
