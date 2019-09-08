using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using SiteParser.Core.Repositories;
using SiteParser.Core.Services;

namespace SiteParser.Tests
{
    [TestClass]
    public class SaveIntoDatabaseServiceTest
    {
        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            //Arrange
            var saver = Substitute.For<ISaver>();
            SaveIntoDatabaseService saveIntoDatabaseService = new SaveIntoDatabaseService(saver);
            string parsedTag = "https://en.wikipedia.org/wiki/Red_fox";
            string expectedString = "Tag has been saved info database.";
            saver.Save(Arg.Is<string>(parsedTag))
                .Returns(expectedString);

            //Act
            var result = saveIntoDatabaseService.SaveUrl(parsedTag);

            //Assert
            saver.Received(1).Save(Arg.Is(parsedTag));
            Assert.AreEqual(expectedString, result, "Tag wasn't save into database.");
        }
    }
}
