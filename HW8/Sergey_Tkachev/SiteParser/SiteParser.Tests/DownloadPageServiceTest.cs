namespace SiteParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using SiteParser.Core.Repositories;
    using SiteParser.Core.Services;

    [TestClass]
    public class DownloadPageServiceTest
    {
        [TestMethod]
        public void ShouldDownloadPage()
        {
            // Arrange
            var downloader = Substitute.For<IDownloader>();
            DownloadPageService downloadPageService = new DownloadPageService(downloader);
            string requestUrl = "https://en.wikipedia.org/wiki/Fox";
            string pageContent = "Foxes have a flattened skull, upright triangular ears, a pointed, slightly upturned snout, and a long bushy tail.";
            string expectedString = "/Resources/Fox.html";
            downloader.Download(Arg.Is<string>(requestUrl))
                .Returns(pageContent);
            downloader.SaveIntoFile(Arg.Is<string>(pageContent))
                .Returns(expectedString);

            // Act
            var result = downloadPageService.DownLoadPage(requestUrl);

            // Assert
            downloader.Received(1).Download(Arg.Is(requestUrl));
            downloader.Received(1).SaveIntoFile(Arg.Is(pageContent));
            Assert.AreEqual(expectedString, result, "Download page service returned wrong result.");
        }
    }
}
