using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trading.Core.Services;
using Trading.Core.Repositories;
using Trading.Core.DataTransferObjects;
using NSubstitute;

namespace Trading.Core.Tests
{
    /// <summary>
    /// Summary description for TradeValidatorTests
    /// </summary>
    [TestClass]
    public class TradeValidatorTests
    {
        IClientRepository clientsRepository;
        IShareRepository shareRepository;
        IClientsSharesRepository clientsSharesRepository;
        ILogger logger;

        [TestInitialize]
        public void Initialize()
        {
            clientsRepository = Substitute.For<IClientRepository>();
            shareRepository = Substitute.For<IShareRepository>();
            clientsSharesRepository = Substitute.For<IClientsSharesRepository>();
            logger = Substitute.For<ILogger>();
            clientsSharesRepository.LoadClientsSharesByID(Arg.Any<ClientsSharesInfo>()).Returns((callInfo) =>
            {
                if (callInfo.Arg<ClientsSharesInfo>().ClientID == 1 && callInfo.Arg<ClientsSharesInfo>().ShareID == 2)
                {
                    return new ClientsSharesEntity()
                    {
                        ClientID = callInfo.Arg<ClientsSharesInfo>().ClientID,
                        ShareID = callInfo.Arg<ClientsSharesInfo>().ShareID,
                        Amount = 15
                    };
                }
                else
                    return null;
            });
            clientsRepository.LoadClientByID(Arg.Any<int>()).Returns((callInfo) =>
            {
                if (callInfo.Arg<int>() == 1 || callInfo.Arg<int>() == 2)
                {
                    return new ClientEntity()
                    {
                        ClientID = callInfo.Arg<int>()
                    };
                }
                else
                    return null;
            });
            shareRepository.LoadShareByID(Arg.Any<int>()).Returns((callInfo) =>
            {
                if (callInfo.Arg<int>() == 1 || callInfo.Arg<int>() == 2)
                {
                    return new ShareEntity()
                    {
                        ShareID = callInfo.Arg<int>()
                    };
                }
                else
                    return null;
            });
        }

        [TestMethod]
        public void ShouldValidateClientInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientRegistrationInfo clientRegistrationInfo = new ClientRegistrationInfo()
            {
                FirstName = "Josh",
                LastName = "Smith",
                PhoneNumber = "80000000000"
            };
            //Act
            var isValid = tradeValidator.ValidateClientInfo(clientRegistrationInfo,logger);
            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void ShouldNotValidateClientInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientRegistrationInfo clientInfo = new ClientRegistrationInfo()
            {
                FirstName = "566",
                LastName = "Smith@gmail.uk",
                PhoneNumber = "Josh"
            };
            //Act
            var isValid = tradeValidator.ValidateClientInfo(clientInfo, logger);
            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ShouldValidateShareInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ShareRegistrationInfo shareInfo = new ShareRegistrationInfo()
            {
                Name = "CostCorp",
                Cost = 200
            };
            //Act
            var isValid = tradeValidator.ValidateShareInfo(shareInfo, logger);
            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void ShouldNotValidateShareInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ShareRegistrationInfo shareInfo = new ShareRegistrationInfo()
            {
                Name = "CostCorp",
                Cost = 0
            };
            //Act
            var isValid = tradeValidator.ValidateShareInfo(shareInfo, logger);
            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ShouldValidateExistsShareToClientInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientsSharesInfo clientShareInfo = new ClientsSharesInfo()
            {
                ClientID = 1,
                ShareID = 2,
                Amount = 20
            };
            //Act
            var isValid = tradeValidator.ValidateShareToClient(clientShareInfo, logger);
            //Assert
            Assert.AreEqual(true, isValid);
            clientsRepository.Received(1).LoadClientByID(clientShareInfo.ClientID);
            shareRepository.Received(1).LoadShareByID(clientShareInfo.ShareID);
            clientsSharesRepository.Received(1).LoadClientsSharesByID(clientShareInfo);
        }
        [TestMethod]
        public void ShouldValidateNotExistsShareToClientInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientsSharesInfo clientShareInfo = new ClientsSharesInfo()
            {
                ClientID = 1,
                ShareID = 1,
                Amount = 20
            };
            //Act
            var isValid = tradeValidator.ValidateShareToClient(clientShareInfo, logger);
            //Assert
            Assert.AreEqual(true, isValid);
            clientsRepository.Received(1).LoadClientByID(clientShareInfo.ClientID);
            shareRepository.Received(1).LoadShareByID(clientShareInfo.ShareID);
            clientsSharesRepository.Received(1).LoadClientsSharesByID(clientShareInfo);
        }

        [TestMethod]
        public void ShouldNotValidateNotExistsShareToClientInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientsSharesInfo clientShareInfo = new ClientsSharesInfo()
            {
                ClientID = 11,
                ShareID = 41,
                Amount = 20
            };
            //Act
            var isValid = tradeValidator.ValidateShareToClient(clientShareInfo, logger);
            //Assert
            Assert.AreEqual(false, isValid);
            clientsRepository.Received(1).LoadClientByID(clientShareInfo.ClientID);
            shareRepository.DidNotReceive().LoadShareByID(clientShareInfo.ShareID);
            clientsSharesRepository.DidNotReceive().LoadClientsSharesByID(clientShareInfo);
        }

        [TestMethod]
        public void ShouldNotValidateNegativeShareToClientInfo()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientsSharesInfo clientShareInfo = new ClientsSharesInfo()
            {
                ClientID = 1,
                ShareID = 2,
                Amount = -30
            };
            //Act
            var isValid = tradeValidator.ValidateShareToClient(clientShareInfo, logger);
            //Assert
            Assert.AreEqual(false, isValid);
            clientsRepository.Received(1).LoadClientByID(clientShareInfo.ClientID);
            shareRepository.Received(1).LoadShareByID(clientShareInfo.ShareID);
            clientsSharesRepository.Received(1).LoadClientsSharesByID(clientShareInfo);
        }

        [TestMethod]
        public void ShouldValidateChangeClientMoney()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            int clientID = 2;
            int amount = 200;
            //Act
            var isValid = tradeValidator.ValidateClientMoney(clientID, amount, logger);
            //Assert
            Assert.AreEqual(true, isValid);
            clientsRepository.Received(1).LoadClientByID(clientID);
        }

        [TestMethod]
        public void ShouldNotValidateChangeClientMoney()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            int clientID = 22;
            int amount = 200;
            //Act
            var isValid = tradeValidator.ValidateClientMoney(clientID, amount, logger);
            //Assert
            Assert.AreEqual(false, isValid);
            clientsRepository.Received(1).LoadClientByID(clientID);
        }

        [TestMethod]
        public void ShouldValidateClientsList()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            IEnumerable<ClientEntity> clients = new List<ClientEntity>()
            {
                new ClientEntity(),
                new ClientEntity()
            };
            //Act
            var isValid = tradeValidator.ValidateClientList(clients, logger);
            //Assert
            Assert.AreEqual(true, isValid);
        }
        [TestMethod]
        public void ShouldNotValidateClientsList()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            IEnumerable<ClientEntity> clients = new List<ClientEntity>();
            //Act
            var isValid = tradeValidator.ValidateClientList(clients, logger);
            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ShouldValidateTradingClient()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientEntity client = new ClientEntity()
            {
                ClientID = 1,
                ClientsShares = new HashSet<ClientsSharesEntity>()
                {
                    new ClientsSharesEntity()
                    {
                        ClientID = 1,
                        ShareID = 2,
                        Amount = 10
                    }
                }
            };
            //Act
            var isValid = tradeValidator.ValidateTradingClient(client, logger);
            //Assert
            Assert.AreEqual(true, isValid);
        }

        [TestMethod]
        public void ShouldNotValidateTradingClient()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientEntity client = new ClientEntity()
            {
                ClientID = 1
            };
            //Act
            var isValid = tradeValidator.ValidateTradingClient(client, logger);
            //Assert
            Assert.AreEqual(false, isValid);
        }

        [TestMethod]
        public void ShouldNotValidateTradingClientWithZeroShares()
        {
            //Arrange
            TradeValidator tradeValidator = new TradeValidator(clientsRepository, shareRepository, clientsSharesRepository);
            ClientEntity client = new ClientEntity()
            {
                ClientID = 1,
                ClientsShares = new HashSet<ClientsSharesEntity>()
                {
                    new ClientsSharesEntity()
                    {
                        ClientID = 1,
                        ShareID = 2,
                        Amount = 0
                    }
                }
            };
            //Act
            var isValid = tradeValidator.ValidateTradingClient(client, logger);
            //Assert
            Assert.AreEqual(false, isValid);
        }
    }
}
