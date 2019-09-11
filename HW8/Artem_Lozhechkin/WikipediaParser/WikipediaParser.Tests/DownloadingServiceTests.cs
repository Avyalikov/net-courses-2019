﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using WikipediaParser.DTO;
using WikipediaParser.Services;

namespace WikipediaParser.Tests
{
    [TestClass]
    public class DownloadingServiceTests
    {
        [TestMethod]
        public void ShouldDownloadPage()
        {
            //Arrange
            HttpClient client = new HttpClient(new HttpMessageHandlerStub());
            DownloadingService downloadingService = new DownloadingService(client);
            LinkInfo linkInfo = new LinkInfo { URL = "https://en.wikipedia.org" };
            //Act
            var filename = downloadingService.DownloadSourceToFile(linkInfo).Result;
            //Assert
            Assert.IsTrue(File.Exists(filename));
            Assert.IsTrue(File.ReadAllText(filename) == File.ReadAllText("forTest.html"));
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Resource not found")]
        public void ShouldNotDownloadPageIfItDoesntExist()
        {
            //Arrange
            HttpClient client = new HttpClient(new HttpMessageHandlerStub());
            DownloadingService downloadingService = new DownloadingService(client);
            LinkInfo linkInfo = new LinkInfo { URL = "http://en.wikipedia.org" };
            //Act
            throw downloadingService.DownloadSourceToFile(linkInfo).Exception.InnerException;
        }
    }
}

