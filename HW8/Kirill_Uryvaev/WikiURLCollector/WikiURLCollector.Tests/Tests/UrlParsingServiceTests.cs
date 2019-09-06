using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiURLCollector.Core.Services;
using System.IO;
using System.Linq;

namespace WikiURLCollector.Tests.Tests
{
    /// <summary>
    /// Summary description for UrlParsingServiceTests
    /// </summary>
    [TestClass]
    public class UrlParsingServiceTests
    {
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            //Arrange
            UrlParsingService urlParsingService = new UrlParsingService();
            var page = File.ReadAllText("Resources\\Emu War - Wikipedia.html");
            //Act
            var tags = urlParsingService.ExtractAllUrlsFromPage(page, 1);
            //Assert
            Assert.AreEqual(54, tags.Count());
            Assert.IsTrue(tags.Where(x => x.URL == "/wiki/United_Kingdom").Count() > 0);
        }
    }
}
