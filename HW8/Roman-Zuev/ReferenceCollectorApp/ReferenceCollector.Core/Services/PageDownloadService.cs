namespace ReferenceCollector.Core.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PageDownloadService : IPageDownloadService
    {
        private readonly HttpClient httpClient;
        private IIoDevice ioDevice;

        public PageDownloadService(IIoDevice ioDevice, HttpClient httpClient)
        {
            this.httpClient = httpClient;

            this.ioDevice = ioDevice;
        }
        public async Task<string> DownloadPage(string url)
        {
            try
            {
                string responseBody = await httpClient.GetStringAsync(url);
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                ioDevice.Print(e.Message);
                return null;
            }
        }

        public string BaseAdress
        {
            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                this.httpClient.BaseAddress = new Uri(value);
            }  
        }
    }
}
