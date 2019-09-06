using System.Threading.Tasks;

namespace WikiURLCollector.Core.Interfaces
{
    public interface IPageDownloadingService
    {
        Task<string> GetPage(string address);
    }
}