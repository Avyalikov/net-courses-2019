using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WikipediaParser.DTO;
using WikipediaParser.Services;

namespace WikipediaParser.Tests
{
    [TestClass]
    public class WikipediaParsingServiceTests
    {
        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            // Arrange
            HttpClient httpClient = new HttpClient(new HttpMessageHandlerStub());
            IDownloadingService downloadingService = new DownloadingService(httpClient);
            IDatasourceManagementService datasource = Substitute.For<IDatasourceManagementService>();
            IPageParsingService pageParsingService = Substitute.For<IPageParsingService>();
            IWikipediaParsingService wikipediaParsingService = new WikipediaParsingService(downloadingService, pageParsingService, datasource);
            pageParsingService.ExtractTagsFromFile(Arg.Any<IUnitOfWork>(), Arg.Is<LinkInfo>(l => l.Level == 0 && l.URL == "https://en.wikipedia.org"))
                .Returns(new List<LinkInfo> { new LinkInfo { URL = "/wiki/abc", Level = 1 }, new LinkInfo { URL = "/wiki/def", Level = 1 }, new LinkInfo { URL = "/wiki/jkl", Level = 1 } });

            wikipediaParsingService.Start("https://en.wikipedia.org");

            pageParsingService.Received(5).ExtractTagsFromFile(Arg.Any<IUnitOfWork>(), Arg.Any<LinkInfo>());
        }
    }
}
