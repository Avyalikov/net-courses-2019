namespace SiteParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using SiteParser.Core.Repositories;
    using SiteParser.Core.Services;

    [TestClass]
    public class IterationTest
    {
        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            // Arrange
            string baseUrl = "https://en.wikipedia.org";
            string path1 = "Resources/test.html";
            string path2 = "Resources/Red_fox.html";
            string initialUrl = "https://en.wikipedia.org/test.html";
            string innerUrl = "https://en.wikipedia.org/wiki/Red_fox";
            string expectedString = "IterationCall() done!";
            string notExpectedString = "Something went wrong in IterationCall().";
            ISaver saver = Substitute.For<ISaver>();
            IDownloader downloader = Substitute.For<IDownloader>();
            ICleaner cleaner = Substitute.For<ICleaner>();
            UrlCollectorService iterationService = new UrlCollectorService(saver, downloader, cleaner);
            downloader.Download(Arg.Is<string>(innerUrl))
               .Returns("someDownloadedHtmlText");
            downloader.Download(Arg.Is<string>(initialUrl))
               .Returns("initialtext");
            downloader.SaveIntoFile(Arg.Is<string>("initialtext"))
                .Returns(path1);
            downloader.SaveIntoFile(Arg.Is<string>("someDownloadedHtmlText"))
                .Returns(path2);

           // Act
            var result = iterationService.Solothread(initialUrl, baseUrl);

            // Assert
            Assert.IsTrue(result.Result.Contains(expectedString), "IterationCall works wrong!");
            Assert.IsFalse(result.Result.Contains(notExpectedString), "IterationCall works wrong!");
        }
    }
}
