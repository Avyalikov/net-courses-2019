using HtmlAgilityPack;

using MultithreadApp.Core.Models;
using MultithreadApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using log4net.Repository.Hierarchy;

namespace MultithreadApp.Core.Services
{
    public class PageService 
    {
        private readonly IPageTableRepository pageRepository;
        private readonly IDownloadWebPageRepository downloadRepository;
        private readonly IExtractHtmlTags extractHtml;
        public PageService (IPageTableRepository pageRepository, IDownloadWebPageRepository downloadRepository, IExtractHtmlTags extractHtml)
        {
            this.pageRepository = pageRepository;
            this.downloadRepository = downloadRepository;
            this.extractHtml = extractHtml;
        }

        public  string DownLoadPage(string url)
        {
            string fileName = "WebPageFile.txt";
            try
            {
                string Page = downloadRepository.DownLoadPage(url);
                try
                {
                    StreamWriter SW = new StreamWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write));
                    SW.Write(Page);
                    SW.Close();

                }
                catch
                {
                    
                }
            }
            catch
            {
                
            }
            return fileName;
        }

        public void Add(PageEntity item)
        {
            if (this.pageRepository.Contains(item))
            {
                //throw new ArgumentException("This link has been already registered.Can't continue");
            }
            else
            {
                this.pageRepository.Add(item);

                this.pageRepository.SaveChanges();
            }
            
        }

        public List<string> ExtractHtmlTags(string fileName)
        {
            List<string> hrefList = extractHtml.ExtractTags(fileName);
            File.Delete(fileName);
            return hrefList;
        }

        public List<string> GetNewBanchOfLinks(int iterationNum)
        {
            List<string> ListToReturn = new List<string>();
            IEnumerable<PageEntity> ListOfPages = this.pageRepository.GetPagesFromPreviousIteration(iterationNum);
            foreach(PageEntity page in ListOfPages)
            {
                Console.WriteLine(page.Link);
                string fileinfo = DownLoadPage(page.Link);           //downloads a webpage to a file
                List<string> listOfLinks =ExtractHtmlTags(fileinfo);
                ListToReturn.AddRange(listOfLinks);
            }
            return ListToReturn;
        }
    }
}
