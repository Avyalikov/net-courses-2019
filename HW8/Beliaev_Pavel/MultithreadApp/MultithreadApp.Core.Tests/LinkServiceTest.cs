using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadApp.Core.DTO;
using MultithreadApp.Core.Model;
using MultithreadApp.Core.Services;
using MultithreadApp.Core.UnitsOfWork;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Tests
{
    [TestClass]
    public class LinkServiceTest
    {
        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            //Arrange
            IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
            UrlService linkService = new UrlService(unitOfWork);
            UrlInfo urlInfo = new UrlInfo
            {
                Link = "https://en.m.wikipedia.org/wiki/Medicine",
                IterationId = 1
            };
            //Act
            linkService.AddLinkToDB(urlInfo);
            //Assert
            unitOfWork.Urls.Received(1).Add(Arg.Is<Url>(w => w.Link == "https://en.m.wikipedia.org/wiki/Medicine" && w.IterationId == 1));
        }
    }
}
