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
        readonly ShareTypeEntity type = new ShareTypeEntity()
        {
            Id = 5,
                Name = "Simple Name",
                Cost = 2700.00M,
                Status = true
        };

        IShareTableRepository shareTableRepository;

        [TestMethod]
        public void ShouldRegisterNewShare()
        {
            // Arrange
            this.shareTableRepository = Substitute.For<IShareTableRepository>();
            SharesService sharesService = new SharesService(this.shareTableRepository);
            ShareRegistrationInfo args = new ShareRegistrationInfo();

            args.CompanyName = "Horns and hooves";
            args.Type = this.type;
            args.Status = true;

            // Act
            var shareId = sharesService.RegisterNewShare(args);

            // Assert
            this.shareTableRepository.Received(1).Add(Arg.Is<ShareEntity>(
                s => s.CompanyName == args.CompanyName
                && s.Type == args.Type
                && s.Status == args.Status));
            this.shareTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewShareIfItExists()
        {
            // Arrange
            this.shareTableRepository = Substitute.For<IShareTableRepository>();
            SharesService sharesService = new SharesService(this.shareTableRepository);
            ShareRegistrationInfo args = new ShareRegistrationInfo();

            args.CompanyName = "Horns and hooves";
            args.Type = this.type;
            args.Status = true;

            // Act
            sharesService.RegisterNewShare(args);

            this.shareTableRepository.Contains(Arg.Is<ShareEntity>( // Now Contains returns true (table contains this share type)
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
            this.shareTableRepository = Substitute.For<IShareTableRepository>();
            int testId = 55;
            this.shareTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share type with this Id)
            SharesService sharesService = new SharesService(this.shareTableRepository);

            // Act
            sharesService.ContainsById(testId); // Try to get share type and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetShareInfo()
        {
            // Arrange
            this.shareTableRepository = Substitute.For<IShareTableRepository>();
            int testId = 55;
            this.shareTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SharesService sharesService = new SharesService(this.shareTableRepository);

            // Act
            var shareInfo = sharesService.GetShare(testId);

            // Assert
            this.shareTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        public void ShouldChangeCompanyName()
        {
            // Arrange
            this.shareTableRepository = Substitute.For<IShareTableRepository>();
            int testId = 55;
            this.shareTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SharesService sharesService = new SharesService(this.shareTableRepository);            
            string newCompanyName = "Seas and oceans";            

            // Act
            sharesService.ChangeCompanyName(testId, newCompanyName);

            // Assert
            this.shareTableRepository.Received(1).SetCompanyName(testId, newCompanyName);
            this.shareTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldChangeShareType()
        {
            // Arrange
            this.shareTableRepository = Substitute.For<IShareTableRepository>();
            int testShareId = 55;
            this.shareTableRepository.ContainsById(Arg.Is(testShareId)).Returns(true);
            SharesService sharesService = new SharesService(this.shareTableRepository);

            // Act            
            sharesService.ChangeType(testShareId, this.type);

            // Assert
            this.shareTableRepository.Received(1).SetType(testShareId, this.type);
            this.shareTableRepository.Received(1).SaveChanges();
        }
    }    
}
