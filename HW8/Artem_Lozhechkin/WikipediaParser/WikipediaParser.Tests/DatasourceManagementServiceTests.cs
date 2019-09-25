namespace WikipediaParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Threading.Tasks;
    using WikipediaParser.DTO;
    using WikipediaParser.Models;
    using WikipediaParser.Repositories;
    using WikipediaParser.Services;

    [TestClass]
    public class DatasourceManagementServiceTests
    {
        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            // Arrange
            DatasourceManagementService datasource = new DatasourceManagementService();
            IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
            ILinksTableRepository tableRepository = Substitute.For<ILinksTableRepository>();
            unitOfWork.LinksTableRepository.Returns(tableRepository);
            string address = "https://en.wikipedia.org";
            tableRepository.ContainsByUrl(Arg.Any<LinkEntity>()).Returns(false);
            // Act
            datasource.AddToDb(unitOfWork, new LinkInfo { Url = address }).Wait();
            // Assert
            unitOfWork.LinksTableRepository.Received(1).AddAsync(
                Arg.Is<LinkEntity>(e => e.Link == address));
        }
        [TestMethod]
        public void ShouldNotSaveTagsIntoDatabaseIfItIsAlreadyThere()
        {
            // Arrange
            DatasourceManagementService datasource = new DatasourceManagementService();
            IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
            ILinksTableRepository tableRepository = Substitute.For<ILinksTableRepository>();
            unitOfWork.LinksTableRepository.Returns(tableRepository);
            string address = "https://en.wikipedia.org";
            tableRepository.ContainsByUrl(Arg.Any<LinkEntity>()).Returns(true);
            // Act
            Task t = datasource.AddToDb(unitOfWork, new LinkInfo { Url = address });
            // Assert
            Assert.AreEqual("Link is already in database", t.Exception.InnerException.Message);
        }
    }
}
