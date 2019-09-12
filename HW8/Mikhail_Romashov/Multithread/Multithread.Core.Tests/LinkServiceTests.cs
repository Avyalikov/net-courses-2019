using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Multithread.Core.Dto;
using Multithread.Core.Models;
using Multithread.Core.Repositories;
using Multithread.Core.Services;
using NSubstitute;

namespace Multithread.Core.Tests
{
    [TestClass]
    public class LinkServiceTests
    {
        ILinkTableRepository linkTableRepository;
        LinkService linkService;

        [TestInitialize]
        public void Initialize()
        {
            linkTableRepository = Substitute.For<ILinkTableRepository>();
            linkService = new LinkService(this.linkTableRepository);
        }
        [TestMethod]
        public void ShouldAddLinkToDatabase()
        {
            //Arrange

            LinkInfo link = new LinkInfo()
            {
                Link = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)",
                Iteration = 1
            };

            //Act
            var linkId = linkService.AddNewLink(link);

            //Assert
            this.linkTableRepository.Received(1).Add(Arg.Is<LinkEntity>(
                w => w.Link == link.Link
                && w.Iteration == link.Iteration));
            this.linkTableRepository.Received(1).SaveChanges();
        }
    }
}
