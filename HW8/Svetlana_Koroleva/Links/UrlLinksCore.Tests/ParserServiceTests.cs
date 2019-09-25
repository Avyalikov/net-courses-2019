using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using UrlLinksCore.IService;
using UrlLinksCore.Services;

namespace UrlLinksCore.Tests
{
    [TestClass]
    public class ParserServiceTests
    {
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            //Arrange
            ParserService parserService = new ParserService();
            var pathToHtml="HTMLPageTest.html";
            string url = "https://en.m.wikipedia.org/wiki/Medicine";
            //Act
            List<string> links=parserService.GetLinksFromHtml(pathToHtml, url);
            //Assert
            Assert.AreEqual(3, links.Count);
        }
    }
}
