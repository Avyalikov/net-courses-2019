using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;


namespace TradingApp.Tests
{
    [TestClass]
    public class ClientModifierTest
    {
        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            //Arrange
           // var clientsTableRep = Substitute.For<ITableRepository>();
            var clientsTableRep2 = Substitute.For<ITableRepository>();
            ClientService clientService = new ClientService( clientsTableRep2);
            ClientInfo clientInfo = new ClientInfo { LastName = "Petrov", FirstName = "Petr", Phone = "1235698", Balance = 1000 };
            //Act
            clientService.AddClientToDB(clientInfo);
            //Assert
            clientsTableRep2.Received(1).Add(Arg.Is<Client>(
                w => w.LastName == "Petrov" && w.FirstName == "Petr" && w.Phone == "1235698" && w.Balance == 1000
                ));
        }
    }
}
