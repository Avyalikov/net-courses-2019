using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HW8;
using HW8.Classes;
using HW8.Intefaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HW8_Tests
{
    [TestClass]
    public class HW8Tests
    {
        [TestMethod]
        public void ShouldExtractHtmlTags()
        {
            string data = @"<!DOCTYPE html>

            <html class=""client-nojs"" lang=""en"" dir=""ltr"">
            <head>
            <meta charset = ""UTF-8""/>
            <title>Test_Page</title>
            </head>
            <body> 
            <a href=""/pages/page1.htm""></a>
            <a href=""/pages/page2.htm""></a>
            <a href=""/pages/page3.htm""></a>
            <a href=""/pages/page4.htm""></a>
            <a href=""/pages/page5.htm""></a>
            <a href=""/pages/page6.htm""></a>
            <a href=""/pages/page7.htm""></a>
            <a href=""/pages/page8.htm""></a>
            </body>
            </html>
            ";

            List<string> checkLinks = new List<string>();
            checkLinks.Add(@"pages/page1.htm");
            checkLinks.Add(@"pages/page2.htm");
            checkLinks.Add(@"pages/page3.htm");
            checkLinks.Add(@"pages/page4.htm");
            checkLinks.Add(@"pages/page5.htm");
            checkLinks.Add(@"pages/page6.htm");
            checkLinks.Add(@"pages/page7.htm");
            checkLinks.Add(@"pages/page8.htm");

            List<string> listOfLinks = new List<string>();

            MatchCollection hrefs = Regex.Matches(data, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

            foreach (Match match in hrefs)
            {
                string value = match.Groups[1].Value;
                Match links = Regex.Match(value, @"href=\""/(.*?)\""", RegexOptions.Singleline);

                if (links.Success)
                {
                    listOfLinks.Add(links.Groups[1].Value);
                }
            }

            Assert.IsTrue(checkLinks.Count == listOfLinks.Count);

            for (int i = 0; i < listOfLinks.Count; i++)
            {
                Assert.IsTrue(listOfLinks[i] == checkLinks[i]);
            }
        }

        [TestMethod]
        public void ShouldSaveTagsIntoDatabase()
        {
            string startingUrl = @"http://www.samplesite.com/pages";
            string link = @"pages/page1.htm";
            IStorageProvider storageProvider = new MockStorageProvider();
            IOutputProvider outputProvider = new MockOutputProvider();
            int recursionLevel = 1;

            Uri uri = new Uri(startingUrl);
            string completeLink = uri.Scheme + @":\\" + new Uri(startingUrl).Authority + @"/" + link;

            if (!storageProvider.Contains(completeLink))
            {
                outputProvider.WriteLine(completeLink + " " + (recursionLevel + 1).ToString());
                storageProvider.TryAdd(completeLink, recursionLevel + 1);
            }

            Assert.IsTrue(storageProvider.GetRecords().Count == 1);
            Assert.IsTrue(storageProvider.GetRecords().ContainsKey(completeLink));

            if (!storageProvider.Contains(completeLink))
            {
                storageProvider.TryAdd(completeLink, recursionLevel + 1);
            }

            Assert.IsTrue(storageProvider.GetRecords().Count == 1);
        }

        [TestMethod]
        public void ShouldDownloadPage()
        {
            string data = string.Empty;
            string test = "Test";
            IClientProvider webClient = new MockClientProvider();

            data = webClient.DownloadString(test);
            webClient.DownloadFile(test, test + ".htm");

            Assert.IsTrue(data == "TestComplete");
            Assert.IsTrue(webClient.PageDownloadCounter == 1);

        }

        [TestMethod]
        public void ShouldCallParsingForEachPageFromPreviousIteration()
        {
            IInputProvider inputProvider = new MockInputProvider();
            IOutputProvider outputProvider = new MockOutputProvider();
            IStorageProvider storageProvider = new MockStorageProvider();
            IClientProvider clientProvider = new MockClientProvider();
            IPageProvider pageProvider = new MockPageProvider();
            ILinkProcessorProvider linkProcessorProvider = new MockLinkProcessorProvider(outputProvider, storageProvider);
            IPageParserProvider pageParserProvider = new MockPageParcerProvider();

            WebPageProcessor wpp = new WebPageProcessor(storageProvider, inputProvider, outputProvider, pageProvider, linkProcessorProvider, pageParserProvider, 0);
            
            wpp.ProcessWebPage("Test", 0);

            Assert.IsTrue(storageProvider.Contains("Test1processed"));
            Assert.IsTrue(storageProvider.Contains("Test2processed"));
            Assert.IsTrue(storageProvider.Contains("Test3processed"));
            Assert.IsTrue(storageProvider.Contains("Test4processed"));
            Assert.IsTrue(storageProvider.Contains("Test5processed"));
            Assert.IsTrue(storageProvider.Contains("Test6processed"));
            Assert.IsTrue(storageProvider.Contains("Test7processed"));
            Assert.IsTrue(storageProvider.Contains("Test8processed"));
        }

        public class MockStorageProvider : IStorageProvider
        {
            public ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>();

            public IReadOnlyDictionary<string, int> GetRecords()
            {
                return dictionary;
            }

            public bool Contains(string link)
            {
                return dictionary.ContainsKey(link);
            }

            public void Clear()
            {
                dictionary.Clear();
            }

            public void Dispose()
            {

            }

            public void SaveChanges()
            {

            }

            public void TryAdd(string link, int recursionLevel)
            {
                dictionary.TryAdd(link, recursionLevel + 1);
            }

            public int Count()
            {
                return dictionary.Count;
            }
        }

        public class MockOutputProvider : IOutputProvider
        {
            public void WriteLine(string str)
            {

            }
        }

        public class MockPageProvider : IPageProvider
        {
            public string GetPage(string url)
            {
                return url;
            }
        }

        public class MockClientProvider : IClientProvider
        {
            int pageDownloadCounter = 0;
            string resultingFileName = string.Empty;

            public int PageDownloadCounter { get => pageDownloadCounter; set => pageDownloadCounter = value; }

            public void DownloadFile(string url, string filename)
            {
                pageDownloadCounter++;
            }

            public string DownloadString(string url)
            {
                return url + "Complete";
            }
        }

        public class MockInputProvider : IInputProvider
        {
            public string ReadLine()
            {
                return "Test";
            }
        }

        public class MockPageParcerProvider : IPageParserProvider
        {
            public List<string> checkLinks = new List<string>();

            public MockPageParcerProvider()
            {
            
            }

            public List<string> GetLinks(string data)
            {
                checkLinks.Clear();

                checkLinks.Add(data + "1");
                checkLinks.Add(data + "2");
                checkLinks.Add(data + "3");
                checkLinks.Add(data + "4");
                checkLinks.Add(data + "5");
                checkLinks.Add(data + "6");
                checkLinks.Add(data + "7");
                checkLinks.Add(data + "8");

                return checkLinks;
            }
        }

        public class MockLinkProcessorProvider : ILinkProcessorProvider
        {
            private IOutputProvider outputProvider;
            private IStorageProvider storageProvider;

            public MockLinkProcessorProvider(IOutputProvider outputProvider, IStorageProvider storageProvider)
            {
                this.outputProvider = outputProvider;
                this.storageProvider = storageProvider;
            }

            public string ProcessLink(string link, int recursionLevel, string startingUrl, object storageLock)
            {
                storageProvider.TryAdd(link + "processed", recursionLevel);

                return link;
            }
        }
    }
}
