namespace stockSimulator.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using stockSimulator.Core.DTO;
    using stockSimulator.Core.Models;
    using stockSimulator.Core.Repositories;
    using stockSimulator.Core.Services;

    [TestClass]
    public class ClientsServiceTest
    {
        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            // Arrange
            var clientTableRepository = Substitute.For<IClientTableRepository>();
            ClientService clientService = new ClientService(clientTableRepository);
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.Name = "Alex";
            args.Surname = "Swift";
            args.PhoneNumber = "+7956159357";
            args.AccountBalance = 9000;

            // Act
            var clientId = clientService.RegisterNewClient(args);

            // Assert
            clientTableRepository.Received(1).Add(Arg.Is<ClientEntity>(
                c => c.Name == args.Name 
                && c.Surname == args.Surname 
                && c.PhoneNumber == args.PhoneNumber
                && c.AccountBalance == args.AccountBalance));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This client has been registered already. Can't continue")]
        public void ShouldNotRegisterNewClientIfItExists()
        {
            // Arrange
            var clientTableRepository = Substitute.For<IClientTableRepository>();
            ClientService clientService = new ClientService(clientTableRepository);
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.Name = "Alex";
            args.Surname = "Swift";
            args.PhoneNumber = "+7956159357";
            args.AccountBalance = 9000;

            // Act
            clientService.RegisterNewClient(args);

            clientTableRepository.Contains(Arg.Is<ClientEntity>(
                c => c.Name == args.Name
                && c.Surname == args.Surname
                && c.PhoneNumber == args.PhoneNumber
                && c.AccountBalance == args.AccountBalance)).Returns(true);

            clientService.RegisterNewClient(args);

            // Assert
        }

        [TestMethod]
        public void ShouldGetClientInfo()
        {
            // Arrange
            var clientTableRepository = Substitute.For<IClientTableRepository>();
            clientTableRepository.ContainsById(Arg.Is(187)).Returns(true);
            ClientService clientService = new ClientService(clientTableRepository);

            // Act
            var client = clientService.GetClient(187);

            // Assert
            clientTableRepository.Received(1).Get(187);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't get client by this Id. May be it has not been registered yet")]
        public void ShouldThrowExeptionIfCantFindClient()
        {
            // Arrange
            var clientTableRepository = Substitute.For<IClientTableRepository>();
            clientTableRepository.ContainsById(Arg.Is(187)).Returns(false);
            ClientService clientService = new ClientService(clientTableRepository);

            // Act
            var client = clientService.GetClient(187);

            // Assert
            clientTableRepository.Received(1).Get(187);
        }
    }
}
