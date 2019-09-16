namespace MultithreadLinksApp.Tests
{
    using MultithreadLinksApp.Core.Models;
    using MultithreadLinksApp.Core.Services;
    using MultithreadLinksApp.Core.Repos;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System.Collections.Generic;

    [TestClass]
    public class URLServiceTest
    {
        IURLRepository urlRepository;
        Dictionary<string, int> urlDictionary = new Dictionary<string, int>()
        {
            {"/wiki/World_War_II", 1},
            {"/wiki/Microsoft", 1},
            {"/wiki/World_War_I", 2}
        };

        [TestInitialize]
        public void Initialize()
        {
            urlRepository = Substitute.For<IURLRepository>();
            urlRepository.GetAllUrls().Returns(new List<URL>() { new URL() { Url = "/wiki/World_War_I", IterationID = 1 } });
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            //Arrange
            URLService urlService = new URLService(urlRepository);
            //Act
            urlService.AddURLFromDict(urlDictionary);
            //Assert
            urlRepository.Received(2).AddURL(Arg.Any<URL>());
            urlRepository.Received(1).GetAllUrls();
        }
    }
}
