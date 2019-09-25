namespace Multithread.Core.Services.Tests
{
    using HtmlAgilityPack;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Multithread.Core.Models;
    using Multithread.Core.Repo;
    using NSubstitute;
    using System.Collections.Generic;
    using System.IO;

    [TestClass()]
    public class LinksServicesTests
    {
        [TestMethod()]
        public void ShouldDowloandPageTest()
        {
            var linkRepo = Substitute.For<ILinksRepo>();
            var webRepo = Substitute.For<IWebRepo>();
            LinkProcessingService linkProcessingService = new LinkProcessingService(linkRepo, webRepo);
            var testUrl = "Resource\\Mummia.html";
            HtmlDocument html = new HtmlDocument
            {
                Text = File.ReadAllText(testUrl)
            };
            webRepo.DowloandPage(testUrl).Returns(html);

            var checkFile = linkProcessingService.DowloandPage(testUrl);

            webRepo.Received(1).DowloandPage(testUrl);
            Assert.AreEqual(checkFile, html);
        }

        [TestMethod()]
        public void ShouldExtraxctHtmlTags()
        {
            var linkRepo = Substitute.For<ILinksRepo>();
            var webRepo = Substitute.For<IWebRepo>();
            LinkProcessingService linkProcessingService = new LinkProcessingService(linkRepo, webRepo);
            var testUrl = "https://en.wikipedia.org/wiki/Mummia";
            HtmlDocument html = new HtmlDocument
            {
                Text = File.ReadAllText("Resource\\Mummia.html")
            };
            webRepo.DowloandPage(testUrl).Returns(html);

            IEnumerable<string> pagesLinks = new List<string>
            {
                "/wiki/Mumu", "https://ru.wikipedia.org/wiki/Ruru",
                "https://en.wikipedia.org/wiki/Trololo", "/wiki/LoLo"
            };

            webRepo.GetAllTagsFromPage(html).Returns(pagesLinks);

            var testResult = linkProcessingService.ExtraxctHtmlTags(testUrl);

            webRepo.Received(1).DowloandPage(testUrl);
            webRepo.Received(1).GetAllTagsFromPage(html);
            Assert.AreEqual(testResult.Count, 3);
        }

        [TestMethod()]
        public void ShouldSaveTagsInDb()
        {
            var linkRepo = Substitute.For<ILinksRepo>();
            var webRepo = Substitute.For<IWebRepo>();
            LinkProcessingService linkProcessingService = new LinkProcessingService(linkRepo, webRepo);

            List<string> testUrls = new List<string> { "https://en.wikipedia.org/wiki/Trololo" };
            linkRepo.Contains(testUrls[0]).Returns(false);
            var testLink = new Link() { Url = testUrls[0], IterationId = 0 };

            linkProcessingService.SaveTagsIntoDb(testUrls);

            linkRepo.Received(1).Contains(testUrls[0]);
        }

        [TestMethod()]
        public void ShouldParsingForEachPage()
        {
            var linkRepo = Substitute.For<ILinksRepo>();
            var webRepo = Substitute.For<IWebRepo>();
            LinkProcessingService linkProcessingService = new LinkProcessingService(linkRepo, webRepo);

            List<string> Urls = new List<string> { "https://en.wikipedia.org/wiki/Trololo" };
            linkRepo.GetAllWithIteration(0).Returns(Urls);
            HtmlDocument html = new HtmlDocument();
            webRepo.DowloandPage(Urls[0]).Returns(html);
            webRepo.GetAllTagsFromPage(html).Returns(new List<string>
            {
                "/wiki/Mumu", "https://ru.wikipedia.org/wiki/Ruru",
                "https://en.wikipedia.org/wiki/Trololo", "/wiki/LoLo"
            });

            linkProcessingService.ParsingForEachPage(2);

            linkRepo.GetAllWithIteration(1).Received(1);
        }

        [TestMethod()]
        public void ShouldRecursiveExit()
        {
            var linkRepo = Substitute.For<ILinksRepo>();
            var webRepo = Substitute.For<IWebRepo>();
            LinkProcessingService linkProcessingService = new LinkProcessingService(linkRepo, webRepo);

            linkProcessingService.ParsingForEachPage(0);

            linkRepo.GetAllWithIteration(0).DidNotReceive();
        }
    }
} 

