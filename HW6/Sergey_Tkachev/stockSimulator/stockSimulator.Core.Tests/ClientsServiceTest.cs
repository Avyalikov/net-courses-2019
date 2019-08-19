using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using stockSimulator.Core.Services;

namespace stockSimulator.Core.Tests
{

    [TestClass]
    public class ClientsServiceTest
    {
        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            //Arrange
            var clientTableRepository = Substitute.For<IClientTableRepository>();
            ClientService clientService = new ClientService(clientTableRepository);
            ClientReservationInfo args = new ClientReservationInfo();
            args.Name = "Alex";
            args.Surname = "Swift";
            args.PhoneNumber = "+7956159357";
            args.AccountBalance = 9000;

            //Act
            var clientId = clientService.RegisterNewClient(args);

            //Assert
            clientTableRepository.Received(1).Add(Arg.Is<ClientEntity>(
                c=>c.Name == args.Name 
                && c.Surname == args.Surname 
                && c.PhoneNumber == args.PhoneNumber
                && c.AccountBalance == args.AccountBalance));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This client has been registered already. Can't continue")]
        public void ShouldNotRegisterNewClientIfItExists()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldGetClientInfo()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't get client by this Id. May it has not been registered yet")]
        public void ShouldThrowExeptionIfCantFindClient()
        {
            throw new NotImplementedException();
        }
    }
}
