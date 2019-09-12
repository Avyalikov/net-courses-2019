using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadApp.Core;
using NSubstitute;
using MultithreadApp.Core.Services;
using MultithreadApp.Core.Repositories;

namespace MultithreadApp.Tests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void ShouldDownloadPage()
        {
            //Arrange
            var pageLoader = Substitute.For<IPageTableRepository>();
            PageService pageService = new PageService();
            string url = " https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";//"C:/Users/Dasha/source/repos/net-courses-2019/HW8/Fedusina Daria/MultithreadApp/MultithreadApp.Core/Source/TestingWebPage.htm";
            //Act
            pageService.DownLoadPage(url);
            //Assert
            pageLoader.Received(1).DownLoadPage(" https://en.wikipedia.org/wiki/The_Mummy_(1999_film)");

        }

        [TestMethod]
        public void ShouldExtractHtmlTags()         //(ресурный файл - пример страницы необходим)
        {
            //Arrange
            var pageLoader = Substitute.For<IPageTableRepository>();
            PageService pageService = new PageService();
            string file = " "; 
            //Act
            pageService.ExtractHtmlTags(file);
            //Assert
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
        }

        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
        }

       
    }
}




    