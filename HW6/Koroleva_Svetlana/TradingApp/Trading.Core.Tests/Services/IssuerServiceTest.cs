using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Core.DTO;
using Trading.Core.Model;


namespace TradingApp.Tests
{
    [TestClass]
    public class IssuerServiceTest
    {
        [TestMethod]
        public void ShouldAddNewIssuer()
        {
            //Arrange
            var issuerTableRepository = Substitute.For<ITableRepository<Issuer>>();
            IssuerService issuerService = new IssuerService(issuerTableRepository);
            IssuerInfo companyInfo = new IssuerInfo
            {
                CompanyName="PJSC KorolevaS Company",
                Address="Lemesos"
            
            };
            //Act
            issuerService.AddIssuer(companyInfo);
            //Assert
            issuerTableRepository.Received(1).Add(Arg.Is<Issuer>(
                w => w.CompanyName == "PJSC KorolevaS Company" && w.Address == "Lemesos"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This issuer exists. Can't continue")]
        public void ShouldNotRegisterNewIssuerIfItExists()
        {
            // Arrange
            var issuerTableRepository = Substitute.For<ITableRepository<Issuer>>();
            IssuerService issuerService = new IssuerService(issuerTableRepository);
            IssuerInfo companyInfo = new IssuerInfo
            {
                CompanyName = "PJSC KorolevaS Company",
                Address = "Lemesos"

            };
            // Act
            issuerService.AddIssuer(companyInfo);

            issuerTableRepository.ContainsDTO(Arg.Is<Issuer>(
                w => w.CompanyName == companyInfo.CompanyName
                && w.Address == companyInfo.Address)).Returns(true);

            issuerService.AddIssuer(companyInfo);
        }

     
        }
    }
