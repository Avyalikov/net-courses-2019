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

            // Act
            clientsService.RegisterNewClient(args);

            this.clientTableRepository.Contains(Arg.Is<ClientEntity>(
                c => c.LastName == args.LastName
                && c.FirstName == args.FirstName
                && c.PhoneNumber == args.PhoneNumber)).Returns(true);

            clientsService.RegisterNewClient(args);

            // Assert
        }

        [TestMethod]
        public void ShouldUpdateClient()
        {
            // Arrange
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            var clientId = 44;
            this.clientTableRepository.ContainsById(Arg.Is(clientId)).Returns(true);
            this.clientTableRepository.Get(Arg.Is(clientId)).Returns(new ClientEntity()
            {
                Id = clientId,
                CreatedAt = DateTime.Now,
                LastName = "Misha",
                FirstName = "Lomonosow",
                PhoneNumber = "+79211234567",
                Status = true
            });
            ClientsService clientsService = new ClientsService(this.clientTableRepository);

            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.LastName = "Michael";
            args.FirstName = "Lomonosov";
            args.PhoneNumber = "+79521234567";

            // Act
            clientsService.UpdateClientData(clientId, args);

            // Assert
            this.clientTableRepository.Received(1).Update(Arg.Is<ClientEntity>(
                c => c.LastName == args.LastName
                && c.FirstName == args.FirstName
                && c.PhoneNumber == args.PhoneNumber));
            this.clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfValidationRegDataFailed()
        {
            // Arrange
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            ClientsService clientsService = new ClientsService(this.clientTableRepository);
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.LastName = "M";
            args.FirstName = "LomonosovLomonosovLom";
            args.PhoneNumber = "0";

            // Act
            clientsService.Validation(args);

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
            clientsService.ContainsById(55);

            // Assert
        }

        [TestMethod]
        public void ShouldRemoveClient()
        {
            // Arrange
            this.clientTableRepository = Substitute.For<IClientTableRepository>();
            this.clientTableRepository.ContainsById(Arg.Is(55)).Returns(true);
            ClientsService clientsService = new ClientsService(this.clientTableRepository);

            // Act
            clientsService.RemoveClient(55);

            // Assert
            this.clientTableRepository.Received(1).Deactivate(55);
            this.clientTableRepository.Received(1).SaveChanges();
        }
    }    
}
