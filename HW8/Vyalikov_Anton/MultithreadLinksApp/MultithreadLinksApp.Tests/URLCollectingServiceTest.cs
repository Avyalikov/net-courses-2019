namespace MultithreadLinksApp.Tests
{
    using MultithreadLinksApp.Core.Interfaces;
    using MultithreadLinksApp.Core.Models;
    using MultithreadLinksApp.Core.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestClass]
    public class URLCollectingServiceTest
    {
        IPageService pageService;
        IURLParsingService urlParsingService;
        [TestInitialize]
        public void Initialize()
        {
            List<URL> urls1 = new List<URL>()
            {
                new URL(){Url = "/wiki/Microsoft"},
                new URL(){Url = "/wiki/World_War_II"},
                new URL(){Url = "/wiki/Xbox"}
            };
            List<URL> urls2 = new List<URL>()
            {
                new URL(){Url = "/wiki/DirectX"},
                new URL(){Url = "/wiki/Windows"},
                new URL(){Url = "/wiki/World_War_I"}
            };
            pageService = Substitute.For<IPageService>();
            urlParsingService = Substitute.For<IURLParsingService>();
            urlParsingService.GetAllURLsFromPage(Arg.Any<string>(), 1).Returns(urls1);
            urlParsingService.GetAllURLsFromPage(Arg.Any<string>(), 2).Returns(urls2);
        }

        [TestMethod]
        public async Task ShouldCallParsingForEachPageFromPreviousIteration()
        {
            //Arrange
            IURLCollectingService urlCollectingService = new URLCollectingService(pageService, urlParsingService);
            string address = "https://en.wikipedia.org/wiki/Emu_War";
            //Act
            var urls = await urlCollectingService.CollectURLs(address, 2);
            //Assert
            Assert.AreEqual(5, urls.Count);
            urlParsingService.Received(4).GetAllURLsFromPage(Arg.Any<string>(), Arg.Any<int>());
            pageService.Received(4).DownloadPage(Arg.Any<string>());
        }
    }
}
