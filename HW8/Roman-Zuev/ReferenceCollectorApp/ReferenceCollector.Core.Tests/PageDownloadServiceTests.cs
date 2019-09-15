namespace ReferenceCollector.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReferenceCollector.Core.Services;
    using NSubstitute;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.IO;
    using System.Net;

    [TestClass]
    public class PageDownloadServiceTests
    {
        IIoDevice ioDevice;
        IPageDownloadService pageDownloadService;

        [TestInitialize]
        public void Initialize()
        {
            ioDevice = Substitute.For<IIoDevice>();
            var mockHandler = new MockHttpMessageHandler();
            var httpClient = new HttpClient(mockHandler);
            pageDownloadService = new PageDownloadService(ioDevice, httpClient);
            pageDownloadService.BaseAdress = "https://en.wikipedia.org";
        }
        private class MockHttpMessageHandler : HttpMessageHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                if (request.RequestUri.Equals("https://en.wikipedia.org/wiki/Strait_of_Gibraltar"))
                {
                    HttpContent page = new StringContent(File.ReadAllText("Resources\\717"));
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = page
                    };
                }

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }
        }
        [TestMethod]
        public void ShouldDownloadPage()
        {
            //Arrange
            string url = "/wiki/Strait_of_Gibraltar";
            string page = File.ReadAllText("Resources\\717");

            //Act
            string testPage = pageDownloadService.DownloadPage(url).Result;

            //Assert
            Assert.AreEqual(page, testPage);
        }
        [TestMethod]
        public void ShouldReturnNullIfPageIsNotFound()
        {
            //Arrange
            string url = "test";
            string page = File.ReadAllText("Resources\\717");

            //Act
            string testPage = pageDownloadService.DownloadPage(url).Result;

            //Assert
            ioDevice.Received(1).Print(Arg.Any<string>());
            Assert.AreEqual(testPage, null);
        }
    }
}
