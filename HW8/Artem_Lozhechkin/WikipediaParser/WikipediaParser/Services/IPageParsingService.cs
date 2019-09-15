namespace WikipediaParser.Services
{
    using System.Collections.Generic;
    using WikipediaParser.DTO;

    public interface IPageParsingService
    {
        List<LinkInfo> ExtractTagsFromFile(IUnitOfWork uof, LinkInfo linkInfo);
        List<LinkInfo> ExtractTagsFromSource(IUnitOfWork uof, LinkInfo linkInfo);
    }
}