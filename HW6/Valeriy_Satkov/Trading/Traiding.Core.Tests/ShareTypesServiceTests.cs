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
            args.Status = true;

            // Act
            var shareTypeId = shareTypesService.RegisterNewShareType(args);

            // Assert
            shareTypeTableRepository.Received(1).Add(Arg.Is<ShareTypeEntity>(
                s => s.Name == args.Name 
                && s.Cost == args.Cost
                && s.Status == args.Status));
            shareTypeTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewShareTypeIfItExists()
        {
            // Arrange
            var shareTypeTableRepository = Substitute.For<IShareTypeTableRepository>();
            ShareTypesService shareTypesService = new ShareTypesService(shareTypeTableRepository);
            ShareTypeRegistrationInfo args = new ShareTypeRegistrationInfo();
            args.Name = "Cheap";
            args.Cost = 1000.00M;
            args.Status = true;

            // Act
            shareTypesService.RegisterNewShareType(args);

            shareTypeTableRepository.Contains(Arg.Is<ShareTypeEntity>( // Now Contains returns true (table contains this share type)
                s => s.Name == args.Name
                && s.Cost == args.Cost
                && s.Status == args.Status)).Returns(true);

            shareTypesService.RegisterNewShareType(args); // Try to reg. same twice and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetShareTypeInfo()
        {
            // Arrange
            var shareTypeTableRepository = Substitute.For<IShareTypeTableRepository>();
            shareTypeTableRepository.ContainsById(Arg.Is(55)).Returns(true);
            ShareTypesService shareTypesService = new ShareTypesService(shareTypeTableRepository);            

            // Act
            var shareTypeInfo = shareTypesService.GetShareType(55);

            // Assert
            shareTypeTableRepository.Received(1).Get(55);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindShareType()
        {
            // Arrange
            var shareTypeTableRepository = Substitute.For<IShareTypeTableRepository>();
            shareTypeTableRepository.ContainsById(Arg.Is(55)).Returns(false); // Now Contains returns false (table don't contains share type with this Id)
            ShareTypesService shareTypesService = new ShareTypesService(shareTypeTableRepository);

            // Act
            shareTypesService.ContainsById(55); // Try to get share type and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldChangeTypeName()
        {
            // Arrange
            var shareTypeTableRepository = Substitute.For<IShareTypeTableRepository>();
            shareTypeTableRepository.ContainsById(Arg.Is(55)).Returns(true);
            ShareTypesService shareTypesService = new ShareTypesService(shareTypeTableRepository);
            string newName = "Test new name of share type";

            // Act
            shareTypesService.ChangeName(55, newName);

            // Assert
            shareTypeTableRepository.Received(1).SetName(55, newName);
            shareTypeTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeCost()
        {
            // Arrange
            var shareTypeTableRepository = Substitute.For<IShareTypeTableRepository>();
            shareTypeTableRepository.ContainsById(Arg.Is(55)).Returns(true);
            ShareTypesService shareTypesService = new ShareTypesService(shareTypeTableRepository);
            decimal newCost = 1000.00M;

            // Act
            shareTypesService.ChangeCost(55, newCost);

            // Assert
            shareTypeTableRepository.Received(1).SetCost(55, newCost);
            shareTypeTableRepository.Received(1).SaveChanges();
        }
    }

    
}
