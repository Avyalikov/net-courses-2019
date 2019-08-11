using trading_software.Services;

namespace trading_software
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            ILoggerService logger = new LoggerService(log4net.LogManager.GetLogger("SampleLogger"));

            IOutputDevice outputDevice = new OutputDevice();
            IInputDevice inputDevice = new InputDevice();
            ITableDrawer tableDrawer = new TableDrawer(outputDevice);
            IClientManager clientManager = new ClientManager(inputDevice, outputDevice, tableDrawer);
            IStockManager stockManager = new StockManager(inputDevice, outputDevice, tableDrawer);
            ITransactionManager transactionManager = new TransactionManager(inputDevice, outputDevice, clientManager, stockManager, tableDrawer);

            TradingEngine tradingEngine = new TradingEngine(outputDevice:outputDevice,
                                                            inputDevice:inputDevice,
                                                            tableDrawer: tableDrawer,
                                                            clientManager: clientManager,
                                                            stockManager: stockManager,
                                                            transactionManager: transactionManager
                                                            );
            tradingEngine.Run();
        }
    }
}
