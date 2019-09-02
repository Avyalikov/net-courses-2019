namespace Multithread.Core.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            throw new NotImplementedException();
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
            parsingService.Add(testLink, testIterationId);

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
            parsingService.AddValidation(testLink, testIterationId);

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
            parsingService.AddValidation(testLink, testIterationId);

            // Assert
        }

        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            throw new NotImplementedException();
        }
    }
}
