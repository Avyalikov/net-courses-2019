using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Interfaces;
using NSubstitute;

namespace MultithreadConsoleApp.Tests
{
    [TestClass]
    public class HtmlReaderTests
    {
        IHtmlReader htmlReader;
        HttpClient httpClient;
        [TestInitialize]
        public void Initialize()
        {
            HttpMessageHandler myHttpMessageHandler = new FakeHttpHandler();
            httpClient = new HttpClient(myHttpMessageHandler);
            htmlReader = new HtmlReader(httpClient);
        }
        public class FakeHttpHandler : HttpMessageHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                if (request.RequestUri.Equals("https://en.wikipedia.org/wiki/The_Mummy_(1999_film)"))
                {
                    string result = File.ReadAllText("Resources\\Mummy1998.txt");
                    HttpContent page = new StringContent(result);
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
            string address = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";

            //Act
            var result = htmlReader.ReadHttp(address);

            //Assert
            var checkEqual = File.ReadAllText("Resources\\Mummy1998.txt");
            Assert.AreEqual(checkEqual, result.Result);
        }

        [TestMethod]
        public void ShouldReturnNullIfBadPage()
        {
            //Arrange
            string address = "https://en.wikipedia.org/wiki/Net_Courses_2019";

            //Act
            var result = htmlReader.ReadHttp(address);

            //Assert
            string checkEqual = null;
            Assert.AreEqual(checkEqual, result.Result);
        }
    }
}