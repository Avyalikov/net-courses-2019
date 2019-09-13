namespace MultithreadLinkParser.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultithreadLinkParser.Core.Services;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    class PageDownloaderServiceTests
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
    }
}