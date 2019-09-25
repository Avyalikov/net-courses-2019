namespace MultithreadApp.Interfaces
{
    using System.Threading.Tasks;

    public interface IFileProvider
    {
        Task SaveToFileAsync(string page, string value);
        string LoadHtml(string page);
    }
}