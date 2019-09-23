namespace SiteParser.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using SiteParser.Core.Models;
    using SiteParser.Core.Repositories;
    using SiteParser.Core.Services;

    [TestClass]
    public class SaveIntoDatabaseServiceTest
    {
        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            // Arrange
            var saver = Substitute.For<ISaver>();
            SaveIntoDatabaseService saveIntoDatabaseService = new SaveIntoDatabaseService(saver);
            string parsedTag = "https://en.wikipedia.org/wiki/Red_fox";
            string expectedString = "Entity was successfully inserted into Database.";
            saver.Save(Arg.Is<LinkEntity>(
                l => l.Link == parsedTag
                && l.IterationID == 5))
                .Returns(expectedString);
            LinkEntity linkToAdd = new LinkEntity()
            {
                IterationID = 5,
                Link = parsedTag
            };

            // Act
            var result = saveIntoDatabaseService.SaveUrl(linkToAdd);

            // Assert
            saver.Received(1).Save(Arg.Is<LinkEntity>(
                l => l.Link == parsedTag
                && l.IterationID == 5));
            Assert.AreEqual(expectedString, result, "Tag wasn't save into database.");
        }
    }
}
