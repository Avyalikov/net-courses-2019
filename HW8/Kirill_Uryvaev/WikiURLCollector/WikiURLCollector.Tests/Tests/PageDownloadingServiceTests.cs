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
    public class PageDownloadingServiceTests
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
                if (request.RequestUri.Equals("https://en.wikipedia.org/wiki/Not_Respond"))
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
            PageDownloadingService pageDownloadingService = new PageDownloadingService(client);
            string address = "https://en.wikipedia.org/wiki/Emu_War";

            //Act
            var page = pageDownloadingService.GetPage(address);
            //Assert
            var truePage = File.ReadAllText("Resources\\Emu War - Wikipedia.html");
            Assert.AreEqual(truePage, page.Result);
        }

        [TestMethod]
        public void ShouldDownloadPageWithSomeLatency()
        {
            //Arrange
            PageDownloadingService pageDownloadingService = new PageDownloadingService(client);
            string address = "https://en.wikipedia.org/wiki/Not_Respond";

            //Act
            var page = pageDownloadingService.GetPage(address);
            //Assert
            var truePage = "Some html content";
            Assert.AreEqual(truePage, page.Result);

        }
        [TestMethod]
        public void ShouldNotDownloadPage()
        {
            //Arrange
            PageDownloadingService pageDownloadingService = new PageDownloadingService(client);
            string address = "https://en.wikipedia.org/wiki/Random";

            //Act
            var page = pageDownloadingService.GetPage(address);
            //Assert
            Assert.IsNull(page.Result);
        }
    }
}
