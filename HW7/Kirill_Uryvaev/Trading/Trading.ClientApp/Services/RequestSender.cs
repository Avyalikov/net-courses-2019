using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Trading.Core.DataTransferObjects;
using Newtonsoft.Json;
using Trading.Core;

namespace Trading.ClientApp
{
    class RequestSender
    {
        private readonly HttpClient client;
        private readonly string baseAddress;
        public RequestSender()
        {
            baseAddress = "http://localhost:9000/";

            // Create HttpCient and make a request to api/values 
            client = new HttpClient();
        }

        public IEnumerable<ClientEntity> GetTop10Clients(int page,out string answer)
        {
            var response = client.GetAsync(baseAddress + $"clients?top=10&page={page.ToString()}").Result;
            answer = response.ToString();
            var responseContent = response.Content.ReadAsStringAsync();
            IEnumerable<ClientEntity> result = null;
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<IEnumerable<ClientEntity>>(responseContent.Result);
            }
            answer += responseContent.Result;
            return result;
        }
        public ClientEntity PostAddClient(ClientRegistrationInfo clientInfo, out string answer)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(clientInfo), Encoding.UTF8, "application/json");
            var response = client.PostAsync(baseAddress + $"clients/add", content).Result;
            answer = response.ToString();
            var responseContent = response.Content.ReadAsStringAsync();
            ClientEntity result = null;
            if (response.IsSuccessStatusCode)
            {
                result = new ClientEntity();
            }
            answer += responseContent.Result;
            return result;
        }
        public string PostUpdateClient(ClientRegistrationInfo clientInfo, int clientID)
        {
            return "";
        }
        public string PostRemoveClient(int clientID)
        {
            return "";
        }
        public string GetClientsShares(int clientID)
        {
            return "";
        }
    }
}
