namespace MultithreadApp.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultithreadApp;
    using MultithreadApp.DataBase;
    using MultithreadApp.DataBase.Model;
    using MultithreadApp.Interfaces;
    using NSubstitute;
    using System.Collections.Generic;

    [TestClass]
    public class LinksServicesTests
    {
        private LinksServices linkService;
        private IDataBase db;
        private IFileProvider file;
        private IHttpProvider wiki;


        [TestInitialize]
        public void Initialize()
        {
            db = Substitute.For<IDataBase>();
            file = Substitute.For<IFileProvider>();
            wiki = Substitute.For<IHttpProvider>();
        }

        [TestMethod]
        public void ShouldDownloadPage()
        {
            // Arrange
            linkService = new LinksServices(db, file, wiki);

            string page = "page_name";

            wiki.GetHtmlAsync(page).Returns("http page"); ;

            // Act
            linkService.DownloadPage(page).Wait();

            // Assert
            wiki.Received(1).GetHtmlAsync(page);
            file.Received(1).SaveToFileAsync(page, "http page");
        }

        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            // Arrange
            linkService = new LinksServices(db, file, wiki);

            string page = "page_name";

            file.LoadHtml(page).Returns(
                @"<!DOCTYPE html>
                <html><body>
                    <div>
                        <a href=""/wiki/Main_Page""></a>
                    </div>
                </body></html>");

            // Act
            var result = linkService.ParseWikiPage(page);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Main_Page", result[0]);
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            // Arrange
            linkService = new LinksServices(db, file, wiki);

            var pages = new List<string>() { "page 1", "page 2" };

            wiki.BaseUrl.Returns(string.Empty);
            db.Links.IsExist(Arg.Any<string>()).Returns(false);

            // Act
            linkService.SavePagesToDb(pages, 1);

            // Assert
            db.Received(1).Connect();
            db.Links.Received(2).Add(Arg.Any<Links>());
            db.Received(1).SaceChanges();
            db.Received(1).Disconnect();
        }

        [TestMethod]
        public void ShouldNotSaveTagsIntoDatabase()
        {
            // Arrange
            linkService = new LinksServices(db, file, wiki);

            var pages = new List<string>() { "page 1", "page 2" };

            wiki.BaseUrl.Returns(string.Empty);
            db.Links.IsExist("page 1").Returns(true);
            db.Links.IsExist("page 2").Returns(false);

            // Act
            linkService.SavePagesToDb(pages, 1);

            // Assert
            db.Received(1).Connect();
            db.Links.Received(1).Add(Arg.Any<Links>());
            db.Received(1).SaceChanges();
            db.Received(1).Disconnect();
        }
        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            // Arrange
            linkService = new LinksServices(db, file, wiki);
            var pages = new List<string>() { "page 1", "page 2" };

            file.LoadHtml("page 1").Returns(
                @"<!DOCTYPE html>
                <html><body>
                    <div>
                        <a href=""/wiki/page 1-1""></a>
                        <a href=""/wiki/page 1-2""></a>
                    </div>
                </body></html>");
            file.LoadHtml("page 2").Returns(
                @"<!DOCTYPE html>
                <html><body>
                    <div>
                        <a href=""/wiki/page 2-1""></a>
                        <a href=""/wiki/page 2-2""></a>
                    </div>
                </body></html>");

            // Act
            var result = linkService.ParseWikiPages(pages);

            // Assert

            //app.Received(1).ParseWikiPage("page 1");
            //app.Received(1).ParseWikiPage("page 2");

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0].Count);
            Assert.AreEqual(2, result[1].Count);

            Assert.AreEqual("page 1-1", result[0][0]);
            Assert.AreEqual("page 1-2", result[0][1]);
            Assert.AreEqual("page 2-1", result[1][0]);
            Assert.AreEqual("page 2-2", result[1][1]);
        }
    }
}