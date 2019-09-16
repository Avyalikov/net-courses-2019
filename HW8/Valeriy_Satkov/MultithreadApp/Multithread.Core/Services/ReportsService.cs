namespace Multithread.Core.Services
{
    using System.Collections.Generic;
    using Multithread.Core.Repositories;

    public class ReportsService
    {
        private readonly ILinkTableRepository linkTableRepository;

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
