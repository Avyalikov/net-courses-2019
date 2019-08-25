using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;
using System.Collections.Generic;

namespace TradingApp.Tests
{
    [TestClass]
    public class ClientServiceTest
    {

       ITableRepository<Client> clientTableRepository;
        [TestInitialize]
        public void Initialize()
        {
            clientTableRepository = Substitute.For<ITableRepository<Client>>();
           clientTableRepository.FindByPK(3).Returns(new Client()
            {
               ClientID = 3,
               LastName = "Petrov",
               FirstName = "Petr",
               Phone = "1235698",
               Balance = 1000,
            });
           
        }



        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            //Arrange
           
            ClientService clientService = new ClientService(clientTableRepository);
            ClientInfo clientInfo = new ClientInfo
            {
                LastName = "Petrov",
                FirstName = "Petr",
                Phone = "1235698",
                Balance = 1000
            };
            //Act
            clientService.AddClientToDB(clientInfo);
            //Assert
            clientTableRepository.Received(1).Add(Arg.Is<Client>(
                w => w.LastName == "Petrov" && w.FirstName == "Petr" && w.Phone == "1235698" && w.Balance == 1000
                ));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This client exists. Can't continue")]
        public void ShouldNotRegisterNewClientIfItExists()
        {
            // Arrange
           
            ClientService clientService = new ClientService(clientTableRepository);
            ClientInfo clientInfo = new ClientInfo
            {
                LastName = "Petrov",
                FirstName = "Petr",
                Phone = "1235698",
                Balance = 1000
            };
            // Act
            clientService.AddClientToDB(clientInfo);

            clientTableRepository.ContainsDTO(Arg.Is<Client>(
                w => w.LastName == clientInfo.LastName
                && w.FirstName == clientInfo.FirstName
                && w.Phone == clientInfo.Phone)).Returns(true);

            clientService.AddClientToDB(clientInfo);
        }

        [TestMethod]
        public void ShouldGetClientInfo()
        {
            // Arrange
            
            ClientService clientService = new ClientService(clientTableRepository);
            clientTableRepository.ContainsByPK(Arg.Is(3)).Returns(true);


            // Act
            var client = clientService.GetEntityByID(3);

            // Assert
            clientTableRepository.Received(1).FindByPK(3);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Client  doesn't exist")]
        public void ShouldThrowExceptionIfCantFindClient()
        {
            // Arrange
           
            ClientService clientService = new ClientService(clientTableRepository);
            clientTableRepository.ContainsByPK(Arg.Is(3)).Returns(false);

            // Act
            var client = clientService.GetEntityByID(3);

        
        }


        [TestMethod]
        public void ShouldEditClientBalance()
        {
            // Arrange
            
            ClientService clientService = new ClientService(clientTableRepository);
            clientTableRepository.ContainsByPK(Arg.Is(3)).Returns(true);
            int clientId = 3;
            decimal sum = 100;
            Client client = new Client()
            {
                ClientID = 3,
                LastName = "Petrov",
                FirstName = "Petr",
                Phone = "1235698",
                Balance = 1000,
            };

            // Act
            clientService.EditClientBalance(clientId, sum);

            // Assert
            Client cl = clientTableRepository.Received(1).FindByPK(clientId);
            client.Balance+=sum;
            clientTableRepository.Received(1).SaveChanges();
            
        }


        [TestMethod]
        public void ShouldGetClientsWithZeroBalance()
        {
            // Arrange
         
            ClientService clientService = new ClientService(clientTableRepository);
          Client client1 = new Client()
            {
                ClientID = 3,
                LastName = "Petrov",
                FirstName = "Petr",
                Phone = "1235698",
                Balance =0,
            };
            Client client2 = new Client()
            {
                ClientID = 4,
                LastName = "Sidorov",
                FirstName = "Petr",
                Phone = "1235598",
                Balance = 0,
            };
           clientTableRepository.Get(o => o.Balance == 0)
                .Returns(new List<Client>{ client1, client2 } );


            // Act
            var client = clientService.GetClientsFromOrangeZone();

            // Assert
            clientTableRepository.Get(o => o.Balance == 0);
        }

        [TestMethod]
        public void ShouldGetClientsWithNegativeBalance()
        {
            // Arrange
           
            ClientService clientService = new ClientService(clientTableRepository);
            Client client1 = new Client()
            {
                ClientID = 3,
                LastName = "Petrov",
                FirstName = "Petr",
                Phone = "1235698",
                Balance = -10,
            };
            Client client2 = new Client()
            {
                ClientID = 4,
                LastName = "Sidorov",
                FirstName = "Petr",
                Phone = "1235598",
                Balance = -2000,
            };
            clientTableRepository.Get(o => o.Balance < 0)
                 .Returns(new List<Client> { client1, client2 });


            // Act
            var client = clientService.GetClientsFromBlackZone();

            // Assert
            clientTableRepository.Get(o => o.Balance <0);
        }

    }
}
