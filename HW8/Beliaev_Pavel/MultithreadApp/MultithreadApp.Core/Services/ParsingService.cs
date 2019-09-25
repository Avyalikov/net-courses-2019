using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Services
{
    public class ParsingService
    {
        public List<string> GetLinksFromHtml(string htmlFilePath, string url)
        {
            Console.WriteLine(htmlFilePath);
            Uri uri = new Uri(url);
            string protocol_domen = url.Replace(uri.LocalPath, string.Empty);
            List<string> links = new List<string>();
            HtmlDocument document = new HtmlDocument();
            try
            {
                document.Load(htmlFilePath);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message + "Incorrect path");
                return null;
            }
            catch (IOException)
            {
                return null;//Threads conflicting                
            }
            HtmlNodeCollection htmlNodes = document.DocumentNode.SelectNodes("//a");
            if (htmlNodes != null)
                foreach (HtmlNode node in htmlNodes)
                {
                    var atribute = node.GetAttributeValue("href", null);
                    if (!(atribute is null))
                    {
                        if (atribute.StartsWith(protocol_domen) && !atribute.Contains("#") && !atribute.Contains("index"))
                        {
                            if (!links.Contains(atribute))
                            {
                                links.Add(atribute.ToString());
                            }
                        }
                        if (atribute.StartsWith("/wiki/") && !atribute.Contains("#") && !atribute.Contains(":"))
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            string linkToAdd = stringBuilder.Append(protocol_domen).Append(atribute).ToString();
                            if (!links.Contains(linkToAdd))
                            {
                                links.Add(linkToAdd);
                            }
                        }
                    }
                }

            return links;
        }
    }
}
