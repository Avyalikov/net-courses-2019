namespace TradingApiClient.Services
{
    public interface IHttpRequestManager
    {
        string Get(string url);

        string Post(string url, string body);
    }
}