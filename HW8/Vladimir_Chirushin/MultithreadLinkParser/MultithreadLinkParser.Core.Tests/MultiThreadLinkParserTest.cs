namespace MultithreadLinkParser.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultithreadLinkParser.Core.Models;
    using MultithreadLinkParser.Core.Repositories;
    using MultithreadLinkParser.Core.Services;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class MultiThreadLinkParserTest
    {
        public class MockHttpMessageHandlerOK : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult<HttpResponseMessage>(
                 new HttpResponseMessage
                 {
                     Content = new StringContent("Test content"),
                     StatusCode = System.Net.HttpStatusCode.OK
                 });
            }
        }
        public class MockHttpMessageHandlerBadRequest : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult<HttpResponseMessage>(
                 new HttpResponseMessage
                 {
                     Content = new StringContent("Test content"),
                     StatusCode = System.Net.HttpStatusCode.BadRequest
                 });
            }
        }

        [TestMethod]
        public void ShouldDownloadPage()
        {
            // Arrange
            var mockHttpMessageHandler = new MockHttpMessageHandlerOK();
            var client = new HttpClient(mockHttpMessageHandler);
            CancellationToken cts = new CancellationToken();
            string urlToParse = "http://en.wikipedia.org/";


            var sut = new PageDownloaderService();

            // Act
            var task = sut.DownloadPage(urlToParse, client, cts);

            // Asserts
            Assert.AreEqual(task.Result, urlToParse.GetHashCode().ToString());
        }


        [TestMethod]
        public void ShouldReturnNullIfNotDownloadPage()
        {
            // Arrange
            var mockHttpMessageHandler = new MockHttpMessageHandlerBadRequest();
            var client = new HttpClient(mockHttpMessageHandler);
            CancellationToken cts = new CancellationToken();
            string urlToParse = "http://en.wikipedia.org/";


            var sut = new PageDownloaderService();

            // Act
            var task = sut.DownloadPage(urlToParse, client, cts);

            // Asserts
            Assert.AreEqual(task.Result, null);
        }

        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            // Arrange
            var pageDownloadService = Substitute.For<IPageDownloaderService>();
            var tagsDataBaseManagerMcok = Substitute.For<ITagsDataBaseManager>();

            string urlToParse = "http://en.wikipedia.org/";
            string stringToParse = @"<!DOCTYPE html>
                <html><body>
                    <div>
                        <a href=""/wiki/Photon""></a>
                    </div>
                </body></html>";

            var sut = new HtmlTagExtractorService(pageDownloadService, tagsDataBaseManagerMcok);

            // Act
            var links = sut.ExtractTags(stringToParse, urlToParse);

            // Asserts
            CollectionAssert.AreEqual(links, new List<string> { "http://en.wikipedia.org/wiki/Photon" });

        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            // Arrange
            var tagsRepositoryMock = Substitute.For<ITagsRepository>();

            var sut = new TagsDataBaseManager(tagsRepositoryMock);
            List<string> linkInfos = new List<string>();
            for (int i = 0; i < 200; i++)
            {
                linkInfos.Add("https://en.wikipedia.org/");
            };
            int linkLayer = 2;
            CancellationToken cts = new CancellationToken();
            tagsRepositoryMock
                .IsExistAsync(Arg.Any<string>())
                .Returns(true);

            // Act
            sut.AddLinksAsync(linkInfos, linkLayer, cts);

            // Asserts
            tagsRepositoryMock
                .Received(1)
                .LinksInsertAsync(Arg.Any<List<LinkInfo>>());
        }

        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            // Arrange
            var tagsDataBaseManagerMcok = Substitute.For<ITagsDataBaseManager>();
            var pageDownloadServiceMock = Substitute.For<IPageDownloaderService>();

            var sut = new HtmlTagExtractorService(pageDownloadServiceMock, tagsDataBaseManagerMcok);
            string linkInfo = "https://en.wikipedia.org/";

            CancellationToken cts = new CancellationToken();
            int linkLayer = 2;

            // Act
            //sut.TagsParser(linkInfo, linkLayer, cts);

            // Asserts
        }
    }
}