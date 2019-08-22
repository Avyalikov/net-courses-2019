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
    public class SharesServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewShare()
        {
            // Arrange
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            SharesService sharesService = new SharesService(shareTableRepository);
            ShareRegistrationInfo args = new ShareRegistrationInfo();

            args.CompanyName = "Horns and hooves";
            args.Type = new ShareTypeEntity()
            {
                Id = 5,
                Name = "Simple Name",
                Cost = 2700.00M,
                Status = true
            };
            args.Status = true;

            // Act
            var shareId = sharesService.RegisterNewShare(args);

            // Assert
            shareTableRepository.Received(1).Add(Arg.Is<ShareEntity>(
                s => s.CompanyName == args.CompanyName
                && s.Type == args.Type
                && s.Status == args.Status));
            shareTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewShareIfItExists()
        {
            // Arrange
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            SharesService sharesService = new SharesService(shareTableRepository);
            ShareRegistrationInfo args = new ShareRegistrationInfo();

            args.CompanyName = "Horns and hooves";
            args.Type = new ShareTypeEntity()
            {
                Id = 5,
                Name = "Simple Name",
                Cost = 2700.00M,
                Status = true
            };
            args.Status = true;

            // Act
            sharesService.RegisterNewShare(args);

            shareTableRepository.Contains(Arg.Is<ShareEntity>( // Now Contains returns true (table contains this share type)
                s => s.CompanyName == args.CompanyName
                && s.Type == args.Type
                && s.Status == args.Status)).Returns(true);

            sharesService.RegisterNewShare(args); // Try to reg. same twice and get exception

            // Assert
        }        

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindShare()
        {
            // Arrange
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            int testId = 55;
            shareTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share type with this Id)
            SharesService sharesService = new SharesService(shareTableRepository);

            // Act
            sharesService.ContainsById(testId); // Try to get share type and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetShareInfo()
        {
            // Arrange
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            int testId = 55;
            shareTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SharesService sharesService = new SharesService(shareTableRepository);

            // Act
            var shareInfo = sharesService.GetShare(testId);

            // Assert
            shareTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        public void ShouldChangeCompanyName()
        {
            // Arrange
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            int testId = 55;
            shareTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SharesService sharesService = new SharesService(shareTableRepository);            
            string newCompanyName = "Seas and oceans";            

            // Act
            sharesService.ChangeCompanyName(testId, newCompanyName);

            // Assert
            shareTableRepository.Received(1).SetCompanyName(testId, newCompanyName);
            shareTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeShareType()
        {
            // Arrange
            var shareTableRepository = Substitute.For<IShareTableRepository>();
            int testShareId = 55;
            shareTableRepository.ContainsById(Arg.Is(testShareId)).Returns(true);
            SharesService sharesService = new SharesService(shareTableRepository);
            ShareTypeEntity newType = new ShareTypeEntity()
            {
                Id = 2,
                Name = "Test ShareTypeName",
                Cost = 5000.00M,
                Status = true
            };            

            // Act            
            sharesService.ChangeType(testShareId, newType);

            // Assert
            shareTableRepository.Received(1).SetType(testShareId, newType);
            shareTableRepository.Received(1).SaveChanges();
        }
    }    
}
