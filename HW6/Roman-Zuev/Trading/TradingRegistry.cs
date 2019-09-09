namespace Trading.DataModel
{
    using StructureMap;
    public class TradingRegistry : Registry
    {
        public TradingRegistry()
        {
            this.For<IPhraseProvider>().Use<JsonPhraseProvider>();
            this.For<IInputOutputDevice>().Use<ConsoleIODevice>();
            this.For<ISettingsProvider>().Use<SettingsProvider>();
            this.For<ITradingCore>().Use<TradingCore>();
            this.For<ITransactionGenerator>().Use<TransactionGenerator>();
            this.For<ITransactionHistoryRecorder>().Use<DBTransactionHistoryRecorder>();

            this.For<Settings>().Use(context => context.GetInstance<ISettingsProvider>().GetSettings());
        }
    }
}