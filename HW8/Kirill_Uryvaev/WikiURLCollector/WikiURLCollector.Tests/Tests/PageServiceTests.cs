using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiURLCollector.Core.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using NSubstitute;

namespace WikiURLCollector.Tests.Tests
{
    /// <summary>
    /// Summary description for PageDownloadingServiceTests
    /// </summary>
    [TestClass]
    public class PageServiceTests
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
                if (request.RequestUri.Equals("https://en.wikipedia.org/wiki/Emu_War"))
                {
                    HttpContent page = new StringContent(File.ReadAllText("Resources\\Emu War - Wikipedia.html"));
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
                    HttpContent page = new StringContent("Some html content");
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
            string address = "https://en.wikipedia.org/wiki/Emu_War";

            //Act
            var fileName = pageDownloadingService.DownloadPageIntoFile(address);
            var page = File.ReadAllText(fileName.Result);
            File.Delete(fileName.Result);
            //Assert
            var truePage = File.ReadAllText("Resources\\Emu War - Wikipedia.html");
            Assert.AreEqual(truePage, page);
        }

        [TestMethod]
        public void ShouldDownloadPageWithSomeLatency()
        {
            //Arrange
            PageService pageDownloadingService = new PageService(client);
            string address = "https://en.wikipedia.org/wiki/Latency";

            //Act
            var fileName = pageDownloadingService.DownloadPageIntoFile(address);
            var page = File.ReadAllText(fileName.Result);
            File.Delete(fileName.Result);
            //Assert
            var truePage = "Some html content";
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
            var fileName = pageDownloadingService.DownloadPageIntoFile(address);
            //Assert
            Assert.IsNotNull(fileName.Exception);
            Assert.AreEqual(1, fileName.Exception.InnerExceptions.Count);
            throw fileName.Exception.InnerException;
        }
    }
}
