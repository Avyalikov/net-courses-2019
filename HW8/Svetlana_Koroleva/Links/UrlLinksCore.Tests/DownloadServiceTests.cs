using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UrlLinksCore.IService;
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
            DownloadService downloadService = Substitute.For<DownloadService>();
            //Act
            downloadService.DownloadHtml(url, filename);

            //Assert
            downloadService.Received(1).DownloadHtml(url,filename);
        }
    }
}
