using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Traiding.Core.Models;
using Traiding.Core.Repositories;
using Traiding.Core.Services;

namespace Traiding.Core.Tests
{
    [TestClass]
    public class ShareTypesServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewShareType()
        {
            // Arrange
            var shareTypeTableRepository = Substitute.For<IShareTypeTableRepository>();
            ShareTypesService shareTypesService = new ShareTypesService(shareTypeTableRepository);
            ShareTypeRegistrationInfo args = new ShareTypeRegistrationInfo();
            args.Name = "Cheap";
            args.Cost = 1000.00M;

            // Act
            var shareTypeId = shareTypesService.RegisterNewShareType(args);

            // Assert
            shareTypeTableRepository.Received(1).Add(Arg.Is<ShareTypeEntity>(
                s => s.Name == args.Name 
                && s.Cost == args.Cost));
            shareTypeTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeTypeName()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldChangeCost()
        {
            throw new NotImplementedException();
        }
    }

    
}
