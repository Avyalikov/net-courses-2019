using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadApp.Core.Services;
using NSubstitute;

namespace MultithreadApp.Core.Tests
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
            downloadService.Received(1).DownloadHtml(url, filename);
        }
    }
}
