namespace MultithreadLinksApp.Tests
{
    using MultithreadLinksApp.Core.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class PageServiceTest
    {
        HttpClient client;

        [TestInitialize]
        public void Initialize()
        {
            HttpMessageHandler mockHttpMessageHandler = new MockHttpMessageHandler();
            client = new HttpClient(mockHttpMessageHandler);
        }

        public class MockHttpMessageHandler : HttpMessageHandler
        {
            int tryNumber = 0;
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                if (request.RequestUri.Equals("https://en.wikipedia.org/wiki/World_War_II"))
                {
                    HttpContent page = new StringContent(File.ReadAllText("Res\\World War II - Wikipedia.html"));
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = page
                    };
                }

                if (request.RequestUri.Equals("https://en.wikipedia.org/wiki/Latency"))
                {
                    tryNumber++;
                    if (tryNumber < 5)
                    {
                        throw new HttpRequestException();
                    }

                    HttpContent page = new StringContent("HTML page");
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
            PageService pageDownloadingService = new PageService(client);
            string address = "https://en.wikipedia.org/wiki/World_War_II";

            //Act
            var fileName = pageDownloadingService.DownloadPage(address);
            var page = File.ReadAllText(fileName.Result);
            File.Delete(fileName.Result);
            //Assert
            var truePage = File.ReadAllText("Res\\World War II - Wikipedia.html");
            Assert.AreEqual(truePage, page);
        }

        [TestMethod]
        public void ShouldDownloadPageWithSomeLatency()
        {
            //Arrange
            PageService pageDownloadingService = new PageService(client);
            string address = "https://en.wikipedia.org/wiki/Latency";

            //Act
            var fileName = pageDownloadingService.DownloadPage(address);
            var page = File.ReadAllText(fileName.Result);
            File.Delete(fileName.Result);
            //Assert
            var truePage = "HTML page";
            Assert.AreEqual(truePage, page);

        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException),"NotFound Not Found")]
        public void ShouldNotDownloadPage()
        {
            //Arrange
            PageService pageDownloadingService = new PageService(client);
            string address = "https://en.wikipedia.org/wiki/Random";

            //Act
            var fileName = pageDownloadingService.DownloadPage(address);
            //Assert
            Assert.IsNotNull(fileName.Exception);
            Assert.AreEqual(1, fileName.Exception.InnerExceptions.Count);
            throw fileName.Exception.InnerException;
        }
    }
}
