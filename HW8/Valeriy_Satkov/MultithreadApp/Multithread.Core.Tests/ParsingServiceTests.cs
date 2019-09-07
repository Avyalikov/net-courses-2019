namespace Multithread.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Moq.Protected;
    using Multithread.Core.Models;
    using Multithread.Core.Repositories;
    using Multithread.Core.Services;
    using NSubstitute;

    [TestClass]
    public class ParsingServiceTests
    {
        HttpMessageHandler testHandler;
        IFileManager testFileManager;

        [TestInitialize]
        public void Initialize()
        {
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            string testContentString = "Hello world";
            int testId = 5;

            // Fake Handler
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                {
                    var message = new HttpResponseMessage(HttpStatusCode.OK);
                    message.Content = new StringContent(testContentString);
                    return message;
                }))
                .Callback<HttpRequestMessage, CancellationToken>((r, c) =>
                {
                    Assert.AreEqual(HttpMethod.Get, r.Method);
                });
            this.testHandler = mockHandler.Object;

            // Fake Streams (FileManager)
            Mock<IFileManager> mockFileManager = new Mock<IFileManager>();
            string fakeFileContents = "Hello world";
            byte[] fakeFileBytes = System.Text.Encoding.UTF8.GetBytes(fakeFileContents);

            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            mockFileManager.Setup(fileManager => fileManager.StreamReader(It.IsAny<string>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));
            this.testFileManager = mockFileManager.Object;
        }
        
        // DownloadPage(...)
        [TestMethod]
        public void ShouldDownloadPage()
        {
            // Arrange            
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            string testContentString = "Hello world";
            int testId = 5;

            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            ParsingService parsingService = new ParsingService(linkTableRepository, this.testFileManager);

            // Act
            var downoloadPageTask = parsingService.DownloadPage(testLink, this.testHandler, testId);

            // Assert
            var finishedString = downoloadPageTask.Result;
            if (finishedString != testContentString)
            {
                throw new ArgumentException("Wrong content");
            }
        }

        // ExtractLinksFromHtmlString(...)
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            throw new NotImplementedException();
            //// Arrange
            //string testInputString = "https://gameofthrones.fandom.com/wiki/Jon_Snow";

            //string[] testStartPageHost = new string[2];
            //int dotComPos = testInputString.IndexOf(".com");
            //testStartPageHost[0] = testInputString.Substring(0, dotComPos + 4); // https://gameofthrones.fandom.com
            //testStartPageHost[1] = testInputString.Substring(dotComPos + 4, 6); // /wiki/

            //string testLink1 = "/wiki/File:Battle_of_the_bastards_Jon_main.jpg";            

            //string htmlContent = "";

            //ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            //ParsingService parsingService = new ParsingService(linkTableRepository);

            //// Act
            //List<string> links = parsingService.ExtractLinksFromHtmlString(ref testStartPageHost, htmlContent);

            //// Assert
            //if (links.Count != 1)
            //{
            //    throw new ArgumentException("Expected one link in list");
            //}
            //if (links[0] != testLink1)
            //{
            //    throw new ArgumentException("Wrong link string in list");
            //}
        }

        // Save(...)
        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            int testIterationId = 7;
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            linkTableRepository.ContainsByLink(Arg.Is(testLink)).Returns(false);
            ParsingService parsingService = new ParsingService(linkTableRepository, this.testFileManager);            

            // Act
            parsingService.Save(testLink, testIterationId);

            // Assert
            linkTableRepository.Received(1).Add(Arg.Is<LinkEntity>(
                u => u.Link == testLink
                && u.IterationId == testIterationId));
            linkTableRepository.Received(1).SaveChanges();
        }

        // ParsingLinksByIterationId(...)
        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            throw new NotImplementedException();
            //// Arrange
            //int testIterationId = 6;
            //ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            //linkTableRepository.EntityListByIterationId(Arg.Is(testIterationId)).Returns(new List<LinkEntity>
            //{
            //    new LinkEntity()
            //    {
            //        Id = 7,
            //        Link = "https://en.wikipedia.org/wiki/The_Expanse_(TV_series)",
            //        IterationId = testIterationId
            //    },
            //    new LinkEntity()
            //    {
            //        Id = 13,
            //        Link = "https://en.wikipedia.org/wiki/James_S._A._Corey",
            //        IterationId = testIterationId
            //    }
            //});
            //ParsingService parsingService = new ParsingService(linkTableRepository);

            // Act
            //parsingService.ParsingLinksByIterationId(testIterationId);

            // Assert
        }

        // ContainsByLink(...)
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCouldFindLinkInDB()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            linkTableRepository.ContainsByLink(Arg.Is(testLink)).Returns(true);
            ParsingService parsingService = new ParsingService(linkTableRepository, this.testFileManager);

            // Act
            parsingService.ContainsByLink(testLink);

            // Assert
        }

        // SaveValidation(...)
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfGotNegativeIterationID()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            int testIterationId = -5;
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            ParsingService parsingService = new ParsingService(linkTableRepository, this.testFileManager);

            // Act
            parsingService.SaveValidation(testLink, testIterationId);

            // Assert
        }
    }
}
