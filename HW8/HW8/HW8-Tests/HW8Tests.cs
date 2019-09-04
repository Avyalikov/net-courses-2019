using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        }

        public class MockOutputProvider : IOutputProvider
        {
            public void WriteLine(string str)
            {

            }
        }
    }
}
