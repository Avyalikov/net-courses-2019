using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadApp;
using NSubstitute;
using MultithreadApp.Core.Services;
using MultithreadApp.Core.Repositories;
using MultithreadApp.Core.Models;
using System.Collections.Generic;

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
            var downLoad = Substitute.For<IDownloadWebPageRepository>();
            var extractHtml = Substitute.For<IExtractHtmlTags>();
            PageService pageService = new PageService(pageLoader, downLoad, extractHtml);
            string url = "testString";
            //Act
            pageService.DownLoadPage(url);
            //Assert
            downLoad.Received(1).DownLoadPage("testString");

        }

        [TestMethod]
        public void ShouldExtractHtmlTags()         //(ресурный файл - пример страницы необходим)
        {
            //Arrange
            var pageLoader = Substitute.For<IPageTableRepository>();
            var downLoad = Substitute.For<IDownloadWebPageRepository>();
            var extractHtml = Substitute.For<IExtractHtmlTags>();
            PageService pageService = new PageService(pageLoader, downLoad, extractHtml);
            string file = "WebPage.xml";
            //Act
            pageService.ExtractHtmlTags(file);                  // to run this tets please go to PageService.cs and comment out 89 line
            //Assert
            extractHtml.Received(1).ExtractTags("WebPage.xml");
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            //Arrange
            var pageLoader = Substitute.For<IPageTableRepository>();
            var downLoad = Substitute.For<IDownloadWebPageRepository>();
            var extractHtml = Substitute.For<IExtractHtmlTags>();
            PageService pageService = new PageService(pageLoader, downLoad, extractHtml);
            PageEntity entity = new PageEntity()                    
            {
                Link = "TestLink",
                IterationId = 1    
            };
            pageLoader.Contains(Arg.Is<PageEntity>(f =>
               f.Link == entity.Link &&
               f.IterationId == entity.IterationId)).Returns(false);

            //Act
            pageService.Add(entity);
            //Assert
            
            pageLoader.Received(1).Add(Arg.Is<PageEntity>( f =>
                f.Link == entity.Link &&
                f.IterationId == entity.IterationId));
        }

        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            //Arrange
            var pageLoader = Substitute.For<IPageTableRepository>();
            var downLoad = Substitute.For<IDownloadWebPageRepository>();
            var extractHtml = Substitute.For<IExtractHtmlTags>();
            PageService pageService = new PageService(pageLoader, downLoad, extractHtml);
            List<PageEntity> testList = new List<PageEntity>();
            PageEntity testEntity = new PageEntity() { Link = "someLink", IterationId = 1 };
            testList.Add(testEntity);
            pageLoader.GetPagesFromPreviousIteration(1).Returns(testList);
            extractHtml.ExtractTags("WebPageFile.txt").Returns(new List<string> { "a", "b", "c" });
            //Act
            pageService.GetNewBanchOfLinks(1);

            //Arrange
            downLoad.Received(1).DownLoadPage("someLink");
        }


    }
}




    