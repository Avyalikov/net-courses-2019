using System;
using System.Collections.Generic;
using MultithreadApp.Core.Models;
using MultithreadApp.Core.Services;

namespace MultithreadApp
{
    public class PageContextInitializer
    {
        public List<PageEntity> ContextInitializer(List<string> listOfLinks)
        {
            var count = 0;
            List<PageEntity> ListOfPages = new List<PageEntity>();
            foreach (string item in listOfLinks)
            {
                count++;
                PageEntity page = new PageEntity();
                page.Link = item;
                page.IterationId = 0;
                ListOfPages.Add(page);
            }
            return ListOfPages;
        }
    }
}