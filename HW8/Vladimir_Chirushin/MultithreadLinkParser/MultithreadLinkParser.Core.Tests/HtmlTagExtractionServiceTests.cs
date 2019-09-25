namespace MultithreadLinkParser.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultithreadLinkParser.Core.Services;
    using NSubstitute;
    using System.Collections.Generic;

    [TestClass]
    class HtmlTagExtractionServiceTests
    {
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            // Arrange
            string urlToParse = "http://en.wikipedia.org/";
            string stringToParse = @"<!DOCTYPE html>
                <html><body>
                    <div>
                        <a href=""/wiki/Photon""></a>
                    </div>
                </body></html>";

            var sut = new HtmlTagExtractorService();

            // Act
            var links = sut.ExtractTags(stringToParse, urlToParse);

            // Asserts
            CollectionAssert.AreEqual(links, new List<string> { "http://en.wikipedia.org/wiki/Photon" });
        }
    }
}