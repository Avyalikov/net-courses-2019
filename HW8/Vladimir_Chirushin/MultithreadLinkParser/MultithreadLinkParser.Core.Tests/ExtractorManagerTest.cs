namespace MultithreadLinkParser.Core.Tests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultithreadLinkParser.Core.Services;
    using NSubstitute;

    [TestClass]
    public class ExtractorManagerTest
    {
        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            // Arrange
            var htmlTagExtractorMock = Substitute.For<IHtmlTagExtractorService>();
            var pageDownloadServiceMock = Substitute.For<IPageDownloaderService>();
            var tagsDataBaseManagerMock = Substitute.For<ITagsDataBaseManager>();

            var sut = new ExtractionManager(htmlTagExtractorMock, pageDownloadServiceMock, tagsDataBaseManagerMock);
            string linkInfo = "https://en.wikipedia.org/wiki/Ryoji_Naraoka";

            CancellationToken cts = new CancellationToken();
            int linkLayer = 1;
            var htmlTagExctractorReturningResult = new List<string>();
            htmlTagExctractorReturningResult.Add("https://en.wikipedia.org/wiki/Racewalking");
            htmlTagExctractorReturningResult.Add("https://en.wikipedia.org/wiki/1936_Summer_Olympics");
            pageDownloadServiceMock
                .DownloadPage(Arg.Any<string>(), Arg.Any<HttpClient>(), Arg.Any<CancellationToken>())
                .Returns(Task.FromResult<string>(linkInfo.GetHashCode().ToString()));

            htmlTagExtractorMock
                .ExtractTags(Arg.Any<string>(), Arg.Any<string>())
                .Returns(htmlTagExctractorReturningResult);


            // Act
            sut.RecursionTagExtraction(linkInfo, linkLayer, cts);

            // Asserts
            pageDownloadServiceMock
                .Received(1)
                .DownloadPage(Arg.Any<string>(), Arg.Any<HttpClient>(), Arg.Any<CancellationToken>());
            htmlTagExtractorMock
                .Received(3)
                .ExtractTags(Arg.Any<string>(), Arg.Any<string>());

        }
    }
}
