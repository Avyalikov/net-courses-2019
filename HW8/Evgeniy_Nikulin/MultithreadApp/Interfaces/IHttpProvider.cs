namespace MultithreadApp.Interfaces
{
    using System.Threading.Tasks;

    public interface IHttpProvider
    {
        string BaseUrl { get; }
        bool IsExist(string page);
        Task<string> GetHtmlAsync(string page);
    }
}