namespace WikipediaParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WikipediaParser.DTO;
    using WikipediaParser.Services;

    [TestClass]
    public class PageParsingServiceTests
    {
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            // Arrange
            PageParsingService pps = new PageParsingService();
            IUnitOfWork uof = Substitute.For<IUnitOfWork>();
            LinkInfo linkInfo = new LinkInfo { FileName = "forTest.html" };
            // Act
            var tags = pps.ExtractTagsFromFile(uof, linkInfo);
            // Assert
            Assert.IsTrue(tags.Count == 5);
        }
    }
}
