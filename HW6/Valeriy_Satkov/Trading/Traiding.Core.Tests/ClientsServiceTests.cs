namespace Traiding.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;
    using Traiding.Core.Services;

    [TestClass]
    public class ClientsServiceTests
    {
        IClientTableRepository clientTableRepository;

        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            // Arrange
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            ClientsService clientsService = new ClientsService(this.clientTableRepository);
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.LastName = "Michael";
            args.FirstName = "Lomonosov";
            args.PhoneNumber = "+79521234567";
            args.Status = true;

            // Act
            var clientId = clientsService.RegisterNewClient(args);

            // Assert
            this.clientTableRepository.Received(1).Add(Arg.Is<ClientEntity>(
                c => c.LastName == args.LastName 
                && c.FirstName == args.FirstName 
                && c.PhoneNumber == args.PhoneNumber
                && c.Status == args.Status));
            this.clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewClientIfItExists()
        {
            // Arrange
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            ClientsService clientsService = new ClientsService(this.clientTableRepository);
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.LastName = "Michael";
            args.FirstName = "Lomonosov";
            args.PhoneNumber = "+79521234567";
            args.Status = true;

            // Act
            clientsService.RegisterNewClient(args);

            this.clientTableRepository.Contains(Arg.Is<ClientEntity>(
                c => c.LastName == args.LastName
                && c.FirstName == args.FirstName
                && c.PhoneNumber == args.PhoneNumber
                && c.Status == args.Status)).Returns(true);

            clientsService.RegisterNewClient(args);

            // Assert
        }

        [TestMethod]
        public void ShouldGetClientInfo()
        {
            // Arrange
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            this.clientTableRepository.ContainsById(Arg.Is(55)).Returns(true);
            ClientsService clientsService = new ClientsService(this.clientTableRepository);

            // Act
            var clientInfo = clientsService.GetClient(55);

            // Assert
            this.clientTableRepository.Received(1).Get(55);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindClient()
        {
            // Arrange
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            this.clientTableRepository.ContainsById(Arg.Is(55)).Returns(false);
            ClientsService clientsService = new ClientsService(this.clientTableRepository);

            // Act
            var clientInfo = clientsService.GetClient(55);

            // Assert
        }
    }    
}
