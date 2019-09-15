namespace ReferenceCollectorAppTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ReferenceCollectorApp.Repositories;
    using ReferenceCollectorApp.Services;
    using ReferenceCollector.Core.Repositories;
    using System.IO;
    using System;

    [TestClass]
    public class WikiReferencesParsingServiceTests
    {
        IFileSystemRepository fileSystemRepository;
        IWikiReferencesParsingService parsingService;

        [TestInitialize]
        public void Initialize()
        {
            fileSystemRepository = new FileSystemRepository();
            parsingService = new WikiReferencesParsingService(fileSystemRepository);
        }

        [TestMethod]
        public void ShouldParseHtmlPageFromFileToDictionary()
        {
            //Arrange
            string test = "/wiki/north_africa";
            bool contains=false;
            
            //Act
            var testDict = parsingService.ParseRefsFromFileToDictionary(Directory.GetCurrentDirectory()+"\\Resources\\717", 0);

            //Assert
            foreach (var item in testDict)
            {
                if (item.Key == test)
                {
                    contains = true;
                }
            }
            Assert.IsTrue(contains);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "File doesn't exist. Trying to find it here: test")]
        public void ShouldNotParseHtmlPageIfFileDoesntExist()
        {
            //Arrange

            //Act
            var testDict = parsingService.ParseRefsFromFileToDictionary("test", 0);

            //Assert

        }
    }
}
