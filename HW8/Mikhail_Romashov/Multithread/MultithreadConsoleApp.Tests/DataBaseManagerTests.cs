using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multithread.Core.Models;
using Multithread.Core.Repositories;
using Multithread.Core.Services;
using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Interfaces;
using NSubstitute;

namespace MultithreadConsoleApp.Tests
{
    [TestClass]
    public class DataBaseManagerTests
    {
        ILinkTableRepository linkTableRepository;
        LinkService linkService;
        IDataBaseManager dataBaseManager;

        [TestInitialize]
        public void Initialize()
        {
            linkTableRepository = Substitute.For<ILinkTableRepository>();
            linkService = Substitute.For<LinkService>(this.linkTableRepository);
            dataBaseManager = new DataBaseManager(this.linkService);
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            //Arrange
            List<string> linksCollection = new List<string>()
            {
                "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)",
                "https://en.wikipedia.org/wiki/The_Terminal",
            };
            int iteration = 1;
            //Act
            dataBaseManager.AddLinksToDB(linksCollection, iteration);

            //Assert

            this.linkTableRepository.Received(2).Contains(Arg.Any<string>());
            this.linkTableRepository.Received(1).Add(Arg.Is<LinkEntity>(
                    w => w.Link == linksCollection[0]
                    && w.Iteration == iteration));
            this.linkTableRepository.Received(1).Add(Arg.Is<LinkEntity>(
                     w => w.Link == linksCollection[1]
                    && w.Iteration == iteration));
            this.linkTableRepository.Received(2).SaveChanges();
        }

        [TestMethod]
        public void ShouldSkipSaveTagsIntoDatabaseIfExists()
        {
            //Arrange
            List<string> linksCollection = new List<string>()
            {
                "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)",
                "https://en.wikipedia.org/wiki/The_Terminal",
            };
            int iteration = 1;
            //Act
            dataBaseManager.AddLinksToDB(linksCollection, iteration);

            //Assert
            this.linkTableRepository.Contains(Arg.Is<string>(
                s => s == "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)")).Returns(true);
            this.linkTableRepository.Contains(Arg.Is<string>(
                s => s == "https://en.wikipedia.org/wiki/The_Terminal")).Returns(true);

            iteration = 2; //change iteration
            dataBaseManager.AddLinksToDB(linksCollection, iteration); // and try to add the same links again

            this.linkTableRepository.Received(4).Contains(Arg.Any<string>());
            this.linkTableRepository.Received(1).Add(Arg.Is<LinkEntity>(
                    w => w.Link == linksCollection[0]
                    && w.Iteration == 1));
            this.linkTableRepository.Received(1).Add(Arg.Is<LinkEntity>(
                     w => w.Link == linksCollection[1]
                    && w.Iteration == 1));
            this.linkTableRepository.Received(2).SaveChanges();
        }
    }
}