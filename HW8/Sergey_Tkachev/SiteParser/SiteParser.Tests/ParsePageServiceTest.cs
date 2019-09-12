using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SiteParser.Core.Repositories;
using SiteParser.Core.Services;

namespace SiteParser.Tests
{
    [TestClass]
    public class ParsePageServiceTest
    {
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            //Arrange
            string baseUrl = "https://en.wikipedia.org";
            string path = "Resources/test.html";
            string expectedString = "https://en.wikipedia.org/wiki/Red_fox";
            string notExpectedString = "https://vk.com/gingerfoxday";
            int iterationId = 5;
            ISaver saver = Substitute.For<ISaver>();
            SaveIntoDatabaseService saveService = new SaveIntoDatabaseService(saver);
            ParsePageService parsePageService = new ParsePageService(saveService);

            //Act
            var result = parsePageService.Parse(path, baseUrl, iterationId);

            //Assert
            Assert.IsTrue(result.Contains(expectedString), "File has been parsed wrong!");
            Assert.IsFalse(result.Contains(notExpectedString), "File has been parsed wrong!");
        }
    }
}
