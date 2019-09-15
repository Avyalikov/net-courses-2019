namespace Multithread.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using Multithread.Core.Models;
    using Multithread.Core.Repositories;

    public class ReportsService
    {
        private ILinkTableRepository linkTableRepository;

        public ReportsService(ILinkTableRepository linkTableRepository)
        {
            this.linkTableRepository = linkTableRepository;
        }

        public Dictionary<string, int> LookingForDuplicatesInDb()
        {
            var list = this.linkTableRepository.LookingForDuplicateLinkStrings();
            if (list == null)
            {
                list = new Dictionary<string, int>();
            }
            return list;
        }
    }
}
