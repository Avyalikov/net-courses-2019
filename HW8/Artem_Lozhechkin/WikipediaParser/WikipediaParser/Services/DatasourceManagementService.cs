namespace WikipediaParser.Services
{
    using System;
    using System.Threading.Tasks;
    using WikipediaParser.DTO;
    using WikipediaParser.Models;

    public class DatasourceManagementService : IDatasourceManagementService
    {
        public async Task AddToDb(IUnitOfWork uof, LinkInfo linkInfo)
        {
            LinkEntity linkEntity = new LinkEntity { IterationId = linkInfo.Level, Link = linkInfo.Url };

            if (!uof.LinksTableRepository.ContainsByUrl(linkEntity))
            {
                await uof.LinksTableRepository.AddAsync(linkEntity);
            }
            else throw new Exception("Link is already in database");
        }
    }
}
