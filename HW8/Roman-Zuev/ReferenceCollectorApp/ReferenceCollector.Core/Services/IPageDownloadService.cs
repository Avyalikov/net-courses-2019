namespace ReferenceCollector.Core.Services
{
    using System.Threading.Tasks;
    public interface IPageDownloadService
    {
        Task<string> DownloadPage(string url);
        string BaseAdress { set; }
    }
}