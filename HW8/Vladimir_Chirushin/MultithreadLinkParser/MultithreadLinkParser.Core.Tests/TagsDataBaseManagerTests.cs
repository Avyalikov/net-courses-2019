namespace MultithreadLinkParser.Core.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MultithreadLinkParser.Core.Models;
    using MultithreadLinkParser.Core.Repositories;
    using MultithreadLinkParser.Core.Services;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Threading;

    [TestClass]
    class TagsDataBaseManagerTests
    {
        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            // Arrange
            var tagsRepositoryMock = Substitute.For<ITagsRepository>();

            var sut = new TagsDataBaseManager(tagsRepositoryMock);
            List<string> linkInfos = new List<string>();
            for (int i = 0; i < 200; i++)
            {
                linkInfos.Add("https://en.wikipedia.org/");
            }

            int linkLayer = 2;
            CancellationToken cts = new CancellationToken();
            tagsRepositoryMock
                .IsExistAsync(Arg.Any<string>())
                .Returns(true);

            // Act
            sut.AddLinksAsync(linkInfos, linkLayer, cts);

            // Asserts
            tagsRepositoryMock
                .Received(1)
                .LinksInsertAsync(Arg.Any<List<LinkInfo>>());
        }
    }
}