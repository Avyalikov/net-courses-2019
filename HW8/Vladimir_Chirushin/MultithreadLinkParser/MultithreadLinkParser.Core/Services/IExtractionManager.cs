namespace MultithreadLinkParser.Core.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IExtractionManager
    {
        Task<bool> MyRecAsync(string urlToParse, int linkLayer, CancellationToken cts);
    }
}