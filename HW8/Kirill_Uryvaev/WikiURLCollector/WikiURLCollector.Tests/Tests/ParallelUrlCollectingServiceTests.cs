using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiURLCollector.Core.Services;
using WikiURLCollector.Core.Interfaces;
using WikiURLCollector.Core.Models;
using NSubstitute;
using System.Threading.Tasks;

namespace WikiURLCollector.Tests.Tests
{
    /// <summary>
    /// Summary description for ParallelUrlCollectingServiceTests
    /// </summary>
    [TestClass]
    public class ParallelUrlCollectingServiceTests
    {
        IPageService pageDownloadingService;
        IUrlParsingService urlParsingService;
        [TestInitialize]
        public void Initialize()
        {
            List<UrlEntity> urls1 = new List<UrlEntity>()
            {
                new UrlEntity(){URL = "/wiki/Dominic_Serventy"},
                new UrlEntity(){URL = "/wiki/World_War_I"},
                new UrlEntity(){URL = "/wiki/United_Kingdom"}
            };
            List<UrlEntity> urls2 = new List<UrlEntity>()
            {
                new UrlEntity(){URL = "/wiki/George_Pearce"},
                new UrlEntity(){URL = "/wiki/Movietone_News"},
                new UrlEntity(){URL = "/wiki/World_War_I"}
            };
            pageDownloadingService = Substitute.For<IPageService>();
            urlParsingService = Substitute.For<IUrlParsingService>();
            urlParsingService.ExtractAllUrlsFromPage(Arg.Any<string>(), 1).Returns(urls1);
            urlParsingService.ExtractAllUrlsFromPage(Arg.Any<string>(), 2).Returns(urls2);
        }

        [TestMethod]
        public async Task ShouldCallParsingForEachPageFromPreviousIteration()
        {
            //Arrange
            ParallelUrlCollectingService urlCollectingService = new ParallelUrlCollectingService(pageDownloadingService, urlParsingService);
            string address = "https://en.wikipedia.org/wiki/Emu_War";
            //Act
            var urls = await urlCollectingService.GetUrls(address, 2);
            //Assert
            Assert.AreEqual(5, urls.Count);
            urlParsingService.Received(4).ExtractAllUrlsFromPage(Arg.Any<string>(), Arg.Any<int>());
            pageDownloadingService.Received(4).DownloadPageIntoFile(Arg.Any<string>());
        }
    }
}
