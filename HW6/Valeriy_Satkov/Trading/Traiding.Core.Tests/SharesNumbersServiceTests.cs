using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Traiding.Core.Dto;
using Traiding.Core.Models;
using Traiding.Core.Repositories;
using Traiding.Core.Services;

namespace Traiding.Core.Tests
{
    [TestClass]
    public class SharesNumbersServiceTests
    {
        [TestMethod]
        public void ShouldCreateNewSharesNumber()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);
            SharesNumberRegistrationInfo args = new SharesNumberRegistrationInfo();
            args.Client = new ClientEntity()
            {
                Id = 5,
                CreatedAt = DateTime.Now,
                FirstName = "John",
                LastName = "Snickers",
                PhoneNumber = "+7956244636652",
                Status = true
            };
            args.Share = new ShareEntity()
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                CompanyName = "Simple Company",
                Type = new ShareTypeEntity()
                {
                    Id = 4,                       
                    Name = "not so cheap",
                    Cost = 1200.00M,
                    Status = true
                },
                Status = true
            };
            args.Number = 20;

            // Act
            var shareId = sharesNumbersService.Create(args);

            // Assert
            sharesNumberTableRepository.Received(1).Add(Arg.Is<SharesNumberEntity>(
                n => n.Client == args.Client
                && n.Share == args.Share
                && n.Number == args.Number));
            sharesNumberTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldNotRegisterNewSharesNumberIfItExists()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);
            SharesNumberRegistrationInfo args = new SharesNumberRegistrationInfo();
            args.Client = new ClientEntity()
            {
                Id = 5,
                CreatedAt = DateTime.Now,
                FirstName = "John",
                LastName = "Snickers",
                PhoneNumber = "+7956244636652",
                Status = true
            };
            args.Share = new ShareEntity()
            {
                Id = 2,
                CreatedAt = DateTime.Now,
                CompanyName = "Simple Company",
                Type = new ShareTypeEntity()
                {
                    Id = 4,
                    Name = "not so cheap",
                    Cost = 1200.00M,
                    Status = true
                },
                Status = true
            };
            args.Number = 20;

            // Act
            sharesNumbersService.Create(args);

            sharesNumberTableRepository.Contains(Arg.Is<SharesNumberEntity>( // Now Contains returns true (table contains shares number of this type for client)
                n => n.Client == args.Client
                && n.Share == args.Share
                && n.Number == args.Number)).Returns(true);

            sharesNumbersService.Create(args); // Try to reg. same twice and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldGetSharesNumberInfo()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            int testId = 55;
            sharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);            

            // Act
            var sharesNumberInfo = sharesNumbersService.Get(testId);

            // Assert
            sharesNumberTableRepository.Received(1).Get(testId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCantFindSharesNumber()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            int testId = 55;
            sharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(false); // Now Contains returns false (table don't contains share number with this Id)
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);

            // Act
            sharesNumbersService.ContainsById(testId); // Try to get shares number and get exception

            // Assert
        }

        [TestMethod]
        public void ShouldChangeNumber()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            int testId = 55;
            sharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);
            int newNumber = 21;

            // Act
            sharesNumbersService.ChangeNumber(testId, newNumber);

            // Assert
            sharesNumberTableRepository.Received(1).ChangeNumber(testId, newNumber);
            sharesNumberTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldRemoveShareNumber()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            int testId = 55;
            sharesNumberTableRepository.ContainsById(Arg.Is(testId)).Returns(true);
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);

            // Act
            sharesNumbersService.Remove(testId);

            // Assert
            sharesNumberTableRepository.Received(1).Remove(testId);
            sharesNumberTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldGetSharesNumbersByClient()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);
            int testClientId = 43;

            // Act
            var sharesNumbersOfClient = sharesNumbersService.GetByClient(testClientId);

            // Assert
            sharesNumberTableRepository.Received(1).GetByClient(testClientId);
        }

        [TestMethod]
        public void ShouldGetSharesNumbersByType()
        {
            // Arrange
            var sharesNumberTableRepository = Substitute.For<ISharesNumberTableRepository>();
            SharesNumbersService sharesNumbersService = new SharesNumbersService(sharesNumberTableRepository);
            int testTypeId = 3;

            // Act
            var sharesNumbersOfType = sharesNumbersService.GetByType(testTypeId);

            // Assert
            sharesNumberTableRepository.Received(1).GetByType(testTypeId);
        }
    }

    

    
}
