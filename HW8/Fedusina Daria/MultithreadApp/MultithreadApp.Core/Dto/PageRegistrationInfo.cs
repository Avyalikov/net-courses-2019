using MultithreadApp.Core.Models;
using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Dto
{
    public class PageRegistrationInfo
    {
        public int num { get; set; }
        public  int count { get; set; }
        public string url { get; set; }
        public PageService pageService { get; set; }
    }
}
