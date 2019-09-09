using System.Threading.Tasks;

namespace WikiURLCollector.Core.Interfaces
{
    public interface IPageService
    {
        Task<string> DownloadPageIntoFile(string address);
        string ReadPageFile(string filePath);
    }
}