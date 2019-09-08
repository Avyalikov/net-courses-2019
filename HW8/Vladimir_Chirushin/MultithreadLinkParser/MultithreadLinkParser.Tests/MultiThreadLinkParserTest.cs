namespace MultithreadLinkParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultithreadLinkParser.Models;
    using MultithreadLinkParser.Repositories;
    using MultithreadLinkParser.Services;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;

    [TestClass]
    public class MultiThreadLinkParserTest
    {
        private const string htmlContent = @"<!DOCTYPE html>
<html>
<body>
<p>An <a href=""/wiki/Orthonormal"" class=""mw-redirect"" title=""Orthonormal"">orthonormal</a> basis for Minkowski space necessarily consists of one timelike and three spacelike unit vectors. If one wishes to work with non-orthonormal bases it is possible to have other combinations of vectors. For example, one can easily construct a (non-orthonormal) basis consisting entirely of null vectors, called a <b>null basis</b>.
</p>
<p><a href=""/wiki/Vector_field"" title=""Vector field"">Vector fields</a> are called timelike, spacelike or null if the associated vectors are timelike, spacelike or null at each point where the field is defined.
</p>
</body>
</html>";

        [TestMethod]
        public void ShouldDownloadPage()
        {
            var linkToDBManagerMock = Substitute.For<ILinkToDBManager>();
            var httpClientMock = Substitute.For<HttpClient>();
            CancellationToken cts = new CancellationToken();

            string urlToParse = "http://www.test.ru/";

            var sut = new LinkParserService(linkToDBManagerMock);

            // Act
            sut.ProcessURL(urlToParse, httpClientMock, cts);

            // Asserts
            httpClientMock.Received(1).GetAsync(urlToParse, cts);
        }

        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            //var linkToDBManagerMock = Substitute.For<ILinkToDBManager>();
            //var httpClientMock = Substitute.For<HttpClient>();
            //CancellationToken cts = new CancellationToken();

            //string urlToParse = "http://www.test.ru/";
            //httpClientMock
            //    .GetAsync(urlToParse, cts)
            //    .Returns(Task.FromResult(
            //        new HttpResponseMessage { Content = Encoding.ASCII.GetBytes(htmlContent))} );

            //var sut = new LinkParserService(linkToDBManagerMock);

            //// Act
            //sut.ProcessURL(urlToParse, httpClientMock, cts);

            //// Asserts
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            var linkRepositoryMock = Substitute.For<ILinksRepository>();

            var sut = new LinkToDBManager(linkRepositoryMock);
            List<LinkInfo> linkInfos = new List<LinkInfo>();
            for (int i = 0; i < 101; i++)
            {
                linkInfos.Add(new LinkInfo
                {
                    urlString = "https://wiki.org/",
                    linkLayer = 2
                });
            }
            CancellationToken cts = new CancellationToken();
            linkRepositoryMock
                .IsExistAsync(Arg.Any<string>())
                .Returns(true);

            // Act
            sut.AddLinksAsync(linkInfos, cts);

            // Asserts
            linkRepositoryMock
                .Received(1)
                .LinksInsertAsync(linkInfos);
        }

        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            throw new NotImplementedException();
        }
    }
}
