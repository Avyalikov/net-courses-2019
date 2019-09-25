namespace MultithreadLinksApp.Core.Interfaces
{
    using System.Threading.Tasks;
    public interface IPageService
    {
        Task<string> DownloadPage(string url);
        string ReadPageFromFile(string path);
    }
}
