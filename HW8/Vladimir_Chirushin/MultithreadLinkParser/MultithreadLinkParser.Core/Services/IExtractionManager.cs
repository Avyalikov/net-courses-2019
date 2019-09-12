namespace MultithreadLinkParser.Core.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IExtractionManager
    {
        Task<bool> RecursionTagExtraction(string urlToParse, int linkLayer, CancellationToken cts);
    }
}