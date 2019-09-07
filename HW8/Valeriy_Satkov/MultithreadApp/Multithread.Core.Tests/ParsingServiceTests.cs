namespace Multithread.Core.Tests
{
    using System;
    using System.Collections.Generic;
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
            int testId = 5;
            
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
            var downoloadPageTask = parsingService.DownloadPage(testLink, testHandler.Object, testId);

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
            // Arrange
            string[] testStartPageHost = new string[1];
            testStartPageHost[0] = "https://awaps.yandex.net";

            string testLink1 = "https://awaps.yandex.net/1/c1/tx21lszVAoU5Fo8Pyi8d5u801OmKYBxm6WN0NZHl0S8jbyoimyHnsfliHjnIc_tO51oAXjPOAEJ73w3x7St1HdQlP9oHcN3iN-lNNXz1pxiSKo6U3WAlAnsXX9e_t5neFTAo+DgL9lWyrQgoh9dW7b-XR+EsQTUhI5o9Qn2NevLRBrYsvK8fL7QpK_tyLbIomKDQbsdBPLVgOHKzFSDPUAA1jb7ZoWFuUIOBXW8Mmokj2iU7gknw4XW_tEuXB3a8uAtu1Yzj0X4ZSDJHNTy4mI7RDE+fwALHBvqqtCWPyq52OO7RwH5BD_tmKbaIRy0YQhkKstlzDsmRw6ovNy1PKJKp6RfmDGn-6h8rL51A1d5xHqPpwKq_aR5Wx4uFr06eoBi+DO-k-XEN5WZycj9geI9gA_A_.htm" ;
            string imgLink1 = "https://awaps.yandex.net/0/c1/tVK-Oiz0m0j0AMEash5AnURBkzyTF9BKY0dPAy395Pjd85Vt4G57swybMF+QA_tsciMoIpSf8YqzVn3LopCbHWs2ElxXpT5xEMR9HZCUpPygSLc73OkyGCfqCJx_tH+Mpwzxo93HBzQ3nUfDHcvIJA5aZCKdzgJohTGCLkYRrnIYTGsrE0Qp-Ih4Z_t5SzOl1lQvytTBplcBNe45PwM-m8VUxnivjKRF8TKJgUoZg8HBEFXSeN8ebpq_tQSlSC9ZgpeDSZjm2ZzRwQ7fE6f4v3HW5MXkgNuGVkLkGHGsXJsx24vyXkjS1_tfNSC4r5WZSTPD-sMSPyh1C+Pdviz02f5toDqEfXwG-YTy-vy-";
            string htmlContent = $"</a></span></div></div></div></div></div></fgn></fwap></eflll></div></div><div class=\"container container__banner container__line\"><div class=\"row third\"><div class=\"b - inline b - inline_banner\"><div id=\"banner\" class=\"b - banner__content\"><div class=\"b - banner__wrap\"><div id=\"bc\"><noscript><a href=\"{testLink1}\" target=\"_blank\" rel=\"noopener\"><img src=\"{imgLink1}\"";

            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            ParsingService parsingService = new ParsingService(linkTableRepository);

            //// Act
            //List<string> links = parsingService.ExtractLinksFromHtmlString(testStartPageHost, htmlContent);

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
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            throw new NotImplementedException();
            // Arrange
            int testIterationId = 6;
            ILinkTableRepository linkTableRepository = Substitute.For<ILinkTableRepository>();
            linkTableRepository.EntityListByIterationId(Arg.Is(testIterationId)).Returns(new List<LinkEntity>
            {
                new LinkEntity()
                {
                    Id = 7,
                    Link = "https://en.wikipedia.org/wiki/The_Expanse_(TV_series)",
                    IterationId = testIterationId
                },
                new LinkEntity()
                {
                    Id = 13,
                    Link = "https://en.wikipedia.org/wiki/James_S._A._Corey",
                    IterationId = testIterationId
                }
            });
            ParsingService parsingService = new ParsingService(linkTableRepository);

            // Act
            //parsingService.ParsingLinksByIterationId(testIterationId);

            // Assert
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
    }
}
