namespace Multithread.Core.Tests
{
    using System;
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
        [TestMethod]
        public void ShouldDownloadPage()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            string testContentString = "Hello world";
            
            var testHandler = new Mock<HttpMessageHandler>();
            testHandler.Protected()
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

            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            ParsingService parsingService = new ParsingService(linkTableRepository);

            // Act
            var downoloadPageTask = parsingService.DownloadPage(testLink, testHandler.Object);

            // Assert
            var finishedString = downoloadPageTask.Result;
            if (finishedString != testContentString)
            {
                throw new ArgumentException("Wrong content");
            }
        }

        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            int testIterationId = 7;
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            linkTableRepository.ContainsByLink(Arg.Is(testLink)).Returns(false);
            ParsingService parsingService = new ParsingService(linkTableRepository);            

            // Act
            parsingService.Save(testLink, testIterationId);

            // Assert
            linkTableRepository.Received(1).Add(Arg.Is<LinkEntity>(
                u => u.Link == testLink
                && u.IterationId == testIterationId));
            linkTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfCouldFindLinkInDB()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            linkTableRepository.ContainsByLink(Arg.Is(testLink)).Returns(true);
            ParsingService parsingService = new ParsingService(linkTableRepository);

            // Act
            parsingService.ContainsByLink(testLink);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfGotNegativeIterationID()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            int testIterationId = -5;
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            ParsingService parsingService = new ParsingService(linkTableRepository);

            // Act
            parsingService.SaveValidation(testLink, testIterationId);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "I didn't get exception it's wrong!")]
        public void ShouldThrowExceptionIfGotZeroIterationID()
        {
            // Arrange
            string testLink = "https://en.wikipedia.org/wiki/The_Mummy_Returns";
            int testIterationId = 0;
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            ParsingService parsingService = new ParsingService(linkTableRepository);

            // Act
            parsingService.SaveValidation(testLink, testIterationId);

            // Assert
        }

        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            throw new NotImplementedException();
        }
    }
}
