namespace WikipediaParser.Services
{
    using System.Threading.Tasks;
    using WikipediaParser.DTO;

    public interface IWikipediaParsingService
    {
        void Start(string baseUrl);
        Task ProcessUrlRecursive(LinkInfo link);
    }
}