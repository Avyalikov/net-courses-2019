using HtmlAgilityPack;
using MultithreadApp.Core.Repositories;
using System;
using System.Collections.Generic;

namespace MultithreadApp.Dependencies
{
    public class ExtractHtmlTags: IExtractHtmlTags
    {
        public List<string> ExtractTags(string fileName)
        {
            List<string> hrefList = new List<string>();
            try
            {
                var doc = new HtmlDocument();
                doc.Load(fileName);

                var links = doc.DocumentNode.Descendants("a");
                foreach (var node in links)
                {
                    var href = node.GetAttributeValue("href", string.Empty);
                    if (href != string.Empty)
                    {
                        hrefList.Add(href);
                    }
                    else
                    {
                        //throw new ArgumentException("Empty link");
                    }

                }
            }
            catch
            {

            }
            return hrefList;
        }
    }
}