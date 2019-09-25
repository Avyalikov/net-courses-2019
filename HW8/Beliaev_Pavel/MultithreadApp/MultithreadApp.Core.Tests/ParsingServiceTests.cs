using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Tests
{
    [TestClass]
    public class ParsingServiceTests
    {
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            //Arrange
            ParsingService parsingService = new ParsingService();
            var pathToHtml = "TestHTMLPage.html";
            string url = "https://en.m.wikipedia.org/wiki/Medicine";
            //Act
            List<string> links = parsingService.GetLinksFromHtml(pathToHtml, url);
            //Assert
            Assert.AreEqual(4, links.Count);
        }
    }
}
