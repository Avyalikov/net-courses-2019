using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using stockSimulator.Core.Services;

namespace stockSimulator.Core.Tests
{
    [TestClass]
    public class EditClientsStoksTest
    {
        [TestMethod]
        public void ShouldEditStocksOfClients()
        {
            //Arrange
            var stockOfClientsTableRepository = Substitute.For<IStockOfClientsTableRepository>();
            EditCleintStockService editCleintStockService = new EditCleintStockService(stockOfClientsTableRepository);
            EditStockOfClientInfo args = new EditStockOfClientInfo()
            {
                Client_ID = 5,
                Stock_ID = 1,
                AmountOfStocks = 15
            };
            int entryId;
            stockOfClientsTableRepository.Contains(Arg.Is<StockOfClientsEntity>(
                s => s.ClientID == args.Client_ID
                && s.StockID == args.Stock_ID
                && s.Amount == args.AmountOfStocks), out Arg.Any<int>())
                .Returns(x => {
                    x[1] = 3;
                    return true;
                });

            //Act
            editCleintStockService.Edit(args);

            //Assert
            stockOfClientsTableRepository.Received(1).Update(Arg.Is(3), Arg.Is<StockOfClientsEntity>(
                c => c.ClientID == args.Client_ID
                && c.StockID == args.Stock_ID
                && c.Amount == args.AmountOfStocks));

            stockOfClientsTableRepository.Received(1).SaveChanges();
        }
    }
}
