namespace TradingSimulator
{
    using StructureMap;
    using Core;
    using Core.Interfaces;
    using Core.Repositories;
    using Core.Services;
    using DataBase;
    using DataBase.Repositories;
    using Components;

    class TradingSimulatorRegistry : Registry
    {
        public  TradingSimulatorRegistry()
        {
            this.For<IController>().Use<Controller>();
            this.For<IInputOutput>().Use<ConsoleIO>();
            this.For<IPhraseProvider>().Use<JsonPhraseProvider>();
            this.For<ISettingsProvider>().Use<SettingsProvider>();
            this.For<ILoggerService>().Use<LoggerService>();
            this.For<ITraderRepository>().Use<TraderRepository>();
            this.For<IShareRepository>().Use<ShareRepository>();
            this.For<ITransactionRepository>().Use<TransactionRepository>();
            this.For<ITraderService>().Use<TraderService>();
            this.For<IShareService>().Use<ShareService>();
            this.For<ITransactionService>().Use<TransactionService>();
            this.For<TradingDbContext>().Use<TradingDbContext>();


            this.For<GameSettings>().Use(context => context.GetInstance<ISettingsProvider>().Get());
        }
    }
}