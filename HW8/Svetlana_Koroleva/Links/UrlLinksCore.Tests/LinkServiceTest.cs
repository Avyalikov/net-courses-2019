using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UrlLinksCore.Services;
using UrlLinksCore.DTO;
using UrlLinksCore.Model;

namespace UrlLinksCore.Tests
{
    /// <summary>
    /// Summary description for LinkServiceTest
    /// </summary>
    [TestClass]
    public class LinkServiceTest
    {
        

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            //Arrange
            IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
            LinkService linkService = new LinkService(unitOfWork);
            LinkDTO linkInfo = new LinkDTO
            {
                Link = "https://en.m.wikipedia.org/wiki/Medicine",
                IterationId = 1
            };
            //Act
            linkService.AddLinkToDB(linkInfo);
            //Assert
            unitOfWork.Links.Received(1).Add(Arg.Is<Link>(w => w.Url == "https://en.m.wikipedia.org/wiki/Medicine" && w.IterationId == 1));
        }
    }
}
