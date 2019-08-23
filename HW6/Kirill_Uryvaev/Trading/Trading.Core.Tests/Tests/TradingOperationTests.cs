using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Services;
using Trading.Core;

namespace Trading.Core.Tests
{
    /// <summary>
    /// Summary description for TradingOperationTests
    /// </summary>
    [TestClass]
    public class TradingOperationTests
    {
        ClientService clientService;
        ShareService shareService;
        ClientsSharesService clientsSharesService;

        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void ShouldBuyAndSellShares()
        {
            //Arrange
            TradingOperationService operationService = new TradingOperationService(
                this.clientService, this.shareService, this.clientsSharesService);

            //Act
            
            //Assert
        }
    }
}
