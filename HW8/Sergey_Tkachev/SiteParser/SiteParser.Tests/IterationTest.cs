using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SiteParser.Core.Repositories;
using SiteParser.Core.Services;

namespace SiteParser.Tests
{
    [TestClass]
    public class IterationTest
    {
        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            //Arrange
            string baseUrl = "https://en.wikipedia.org";
            string path = "Resources/test.html";
            string expectedString = "IterationCall() done!";
            string notExpectedString = "Something went wrong in IterationCall().";
            ISaver saver = Substitute.For<ISaver>();
            IDownloader downloader = Substitute.For<IDownloader>();
            CallParsingFromPreviousIterationService iterationService = new CallParsingFromPreviousIterationService(saver, downloader);
            downloader.Download(Arg.Is<string>("https://en.wikipedia.org/wiki/Red_fox"))
               .Returns("someDownloadedHtmlText");
            downloader.SaveIntoFile(Arg.Is<string>("someDownloadedHtmlText"))
                .Returns("Resources/Red_fox.html");

            //Act
            var result = iterationService.IterationCall(path, baseUrl);

            //Assert
            Assert.IsTrue(result.Contains(expectedString), "IterationCall works wrong!");
            Assert.IsFalse(result.Contains(notExpectedString), "IterationCall works wrong!");
        }
    }
}
