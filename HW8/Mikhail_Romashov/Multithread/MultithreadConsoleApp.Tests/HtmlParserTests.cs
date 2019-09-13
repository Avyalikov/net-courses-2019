using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultithreadConsoleApp.Components;
using MultithreadConsoleApp.Interfaces;
using NSubstitute;

namespace MultithreadConsoleApp.Tests
{
    [TestClass]
    public class HtmlParserTests
    {
        IHtmlParser htmlParser;
        [TestInitialize]
        public void Initialize()
        {
            htmlParser = new HtmlParser();
        }

        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            //Arrange
            string html = File.ReadAllText("Resources\\Mummy1998.txt");
            List<string> checkEqual = new List<string>
            {
                "https://en.wikipedia.org/wiki/Saturn_Award_for_Best_Costume",
                "https://en.wikipedia.org/wiki/Motion_Picture_Sound_Editors"
            };

            //Act
            List<string> result = htmlParser.FindLinksFromHtml(html);

            //Assert
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], checkEqual[0]);
            Assert.AreEqual(result[1], checkEqual[1]);
        }

        [TestMethod]
        public void ShouldReturnZeroListAfterExtractBadHtmlTags()
        {
            //Arrange\
            string html = "<!DOCTYPE html><html class=\"client-nojs\" lang=\"en\" dir=\"ltr\"><body>\"</body></html>";

            //Act
            List<string> result = htmlParser.FindLinksFromHtml(html);

            //Assert
            Assert.AreEqual(result.Count, 0);
        }
    }
}