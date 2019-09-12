using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WikipediaParser.DTO;
using WikipediaParser.Models;

namespace WikipediaParser.Services
{
    public class DatasourceManagementService : IDatasourceManagementService
    {
        public async Task AddToDb(IUnitOfWork uof, LinkInfo linkInfo)
        {
            LinkEntity linkEntity = new LinkEntity { IterationId = linkInfo.Level, Link = linkInfo.URL };

            if (!(await uof.LinksTableRepository.ContainsByUrl(linkEntity)))
            {
                await uof.LinksTableRepository.AddAsync(linkEntity);
            }
            else throw new Exception("Link is already in database");
        }
    }
}
