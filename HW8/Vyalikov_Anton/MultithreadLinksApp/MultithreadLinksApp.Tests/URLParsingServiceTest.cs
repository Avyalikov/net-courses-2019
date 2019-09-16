namespace MultithreadLinksApp.Tests
{
    using MultithreadLinksApp.Core.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Linq;

    [TestClass]
    public class URLParsingServiceTest
    {
        [TestMethod]
        public void ShouldExtractHTMLTags()
        {
            //Arrange
            URLParsingService urlParsingService = new URLParsingService();
            var page = File.ReadAllText("Res\\World War II - Wikipedia.html");
            //Act
            var tags = urlParsingService.GetAllURLsFromPage(page, 1);
            //Assert
            Assert.AreEqual(54, tags.Count());
            Assert.IsTrue(tags.Where(x => x.Url == "/wiki/Poland").Count() > 0);
        }
    }
}
