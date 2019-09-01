namespace TradingApiClient.Services
{
    using System.Net.Http;

    public interface IHttpRequestManager
    {
        HttpResponseMessage PostAsync(string url, HttpContent contentString);

        HttpResponseMessage GetAsync(string url);
    }
}