using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiURLCollector.Core.Services;
using WikiURLCollector.Core.Repositories;
using WikiURLCollector.Core.Models;
using NSubstitute;

namespace WikiURLCollector.Tests.Tests
{
    /// <summary>
    /// Summary description for UrlServiceTests
    /// </summary>
    [TestClass]
    public class UrlServiceTests
    {
        IUrlRepository urlRepository;
        Dictionary<string, int> urlDictionary = new Dictionary<string, int>()
        {
            {"/wiki/Dominic_Serventy", 1},
            {"/wiki/Zulus", 1},
            {"/wiki/World_War_I", 2}
        };
        [TestInitialize]
        public void Initialize()
        {
            urlRepository = Substitute.For<IUrlRepository>();
            urlRepository.GetAllUrls().Returns(new List<UrlEntity>() { new UrlEntity() {URL = "/wiki/World_War_I",IterationId = 1 } });
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            //Arrange
            UrlService urlService = new UrlService(urlRepository);
            //Act
            urlService.AddUrlDictionary(urlDictionary);
            //Assert
            urlRepository.Received(2).AddUrlEntity(Arg.Any<UrlEntity>());
            urlRepository.Received(1).GetAllUrls();
        }
    }
}
