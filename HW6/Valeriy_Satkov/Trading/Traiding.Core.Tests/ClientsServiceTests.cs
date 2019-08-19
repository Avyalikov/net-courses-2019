using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Traiding.Core.Models;
using Traiding.Core.Repositories;
using Traiding.Core.Services;

namespace Traiding.Core.Tests
{
    [TestClass]
    public class ClientsServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewClient()
        {
            // Arrange
            var clientTableRepository = Substitute.For<IClientTableRepository>();
            ClientsService clientsService = new ClientsService(clientTableRepository);
            ClientRegistrationInfo args = new ClientRegistrationInfo();
            args.LastName = "Ivoanov";
            args.FirstName = "Ivan";
            args.PhoneNumber = "+79521234567";

            // Act
            var clientId = clientsService.RegisterNewClient(args);

            // Assert
            clientTableRepository.Received(1).Add(Arg.Is<ClientEntity>(
                c => c.LastName == args.LastName 
                && c.FirstName == args.FirstName 
                && c.PhoneNumber == args.PhoneNumber));
            clientTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldGetClientInfo()
        {
            throw new NotImplementedException();
        }
    }

    
}
