using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UrlLinksCore.Services;

namespace UrlLinksCore.Tests
{
    [TestClass]
    public class DownloadServiceTests
    {
       
        [TestMethod]
        public void ShouldDownloadPage()
        {
            //Arrange
            string url = "https://en.m.wikipedia.org/wiki/Medicine";
            string filename = "file1.html";
            WebClient client = Substitute.For<WebClient>();
            DownloadService downloadService = Substitute.For<DownloadService>();
            downloadService.When(w => w.DownloadHtml(url, filename)).Do((callback) =>
            {
                callback.ReceivedWithAnyArgs();
            });
            

            //Act
            downloadService.DownloadHtml(url, filename);

            //Assert

            //downloadService.Received(1).DownloadHtml(url,filename);
            var calls=downloadService.ReceivedCalls();
        }
    }
}
