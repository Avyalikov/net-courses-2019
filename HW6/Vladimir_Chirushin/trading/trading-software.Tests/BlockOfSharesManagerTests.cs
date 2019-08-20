namespace trading_software.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;

    [TestClass]
    public class BlockOfSharesManagerTests
    {
        [TestMethod]
        public void AddShareBlockWithParametersTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var stockRepositoryMock = Substitute.For<IStockRepository>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                stockRepositoryMock);

            BlockOfShares block = new BlockOfShares
            {
                ClientID = 1,
                StockID = 2,
                Amount = 3
            };
            int clientID = 1;
            int stockID = 2;
            int amount = 3;

            // Act
            sut.AddShare(clientID, stockID, amount);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).Add(Arg.Is<BlockOfShares>(b => b.StockID == block.StockID &&
                                                                          b.ClientID == block.ClientID &&
                                                                          b.Amount == block.Amount));
        }
        
        [TestMethod]
        public void AddShareBlockTest()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var stockRepositoryMock = Substitute.For<IStockRepository>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                stockRepositoryMock);

            BlockOfShares block = new BlockOfShares
            {
                ClientID = 1,
                StockID = 2,
                Amount = 3
            };

            // Act
            sut.AddShare(block);

            // Asserts
            blockOfSharesRepositoryMock.Received(1).Add(Arg.Is<BlockOfShares>(b => b.StockID == block.StockID &&
                                                                      b.ClientID == block.ClientID &&
                                                                      b.Amount == block.Amount));
        }
        /*
        [TestMethod]
        public void ManualAddNewShareTest()
        {

            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var dataBaseDevice = Substitute.For<IDataBaseDevice>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                dataBaseDevice);

            BlockOfShares block = new BlockOfShares
            {
                ClientID = 1,
                StockID = 2,
                Amount = 3
            };

            Dictionary<string, string> answers = new Dictionary<string, string>
            {
                { "StockType", "Umbrella" },
                { "ClientName", "Martin Eden" },
                { "Amount", "31415" }
            };

            // Act
            sut.ManualAddNewShare();

            // Asserts
            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write Stock Type:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["StockType"];
            });
            dataBaseDevice
                .GetStockID(Arg.Is<string>(w => w == "Umbrella"))
                .Returns((info) => (int) 35);

            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write client name:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["phone"];
            });
            dataBaseDevice.Received(1)
                .GetClientID(Arg.Is<string>(w => w == "Martin Eden"))
                .Returns((info) =>
                {
                    return 1;
                });

            outpuDeviceMock.Received(1).WriteLine(Arg.Is<string>(w => w == "Write stocks amount:"));
            inputDeviceMock.ReadLine().Returns((info) =>
            {
                return answers["phone"];
            });
        }
        */

        [TestMethod]
        public void CreateRandomShare()
        {
            // Arrange
            var outpuDeviceMock = Substitute.For<IOutputDevice>();
            var inputDeviceMock = Substitute.For<IInputDevice>();
            var tableDrawerMock = Substitute.For<ITableDrawer>();
            var blockOfSharesRepositoryMock = Substitute.For<IBlockOfSharesRepository>();
            var clientRepositoryMock = Substitute.For<IClientRepository>();
            var stockRepositoryMock = Substitute.For<IStockRepository>();

            var sut = new BlockOfSharesManager(
                inputDeviceMock,
                outpuDeviceMock,
                tableDrawerMock,
                blockOfSharesRepositoryMock,
                clientRepositoryMock,
                stockRepositoryMock);


            const int maxNumberOfClients = 7;
            const int maxNumberOfStocks = 12;
            const int maxAmountOfShares = 16;


            clientRepositoryMock
                .GetNumberOfClients()
                .Returns(maxNumberOfClients);
            stockRepositoryMock
                .GetNumberOfStocks()
                .Returns(maxNumberOfStocks);


            // Act
            sut.CreateRandomShare();


            // Asserts
            blockOfSharesRepositoryMock
                .Received(1)
                .Add(Arg.Is<BlockOfShares>(bos => bos.ClientID < maxNumberOfClients &&
                                                 bos.StockID < maxNumberOfStocks &&
                                                 bos.Amount < maxAmountOfShares)
                );
        }
    }
}