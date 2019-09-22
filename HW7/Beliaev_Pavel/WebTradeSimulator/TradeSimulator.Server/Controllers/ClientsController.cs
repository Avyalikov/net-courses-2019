using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Core.Dto;
using TradeSimulator.Core.Services;

namespace TradeSimulator.Server.Controllers
{
    [Route("clients")]
    [ApiController]

    public class ClientsController
    {
        private readonly ClientsService clientsService;
        private readonly ShowDbInfoService showDbInfoService;

        public ClientsController(ClientsService clientsService, ShowDbInfoService showDbInfoService)
        {
            this.clientsService = clientsService;
            this.showDbInfoService = showDbInfoService;
        }
        
        [Route("add")]
        [HttpPost]
        public ActionResult<HttpResponseMessage> RegisterUser([FromBody] RegInfo regInfo)
        {            
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {     
                this.clientsService.Register(regInfo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return response;
            }
        }

        [Route("update")]
        [HttpPost]
        public ActionResult<HttpResponseMessage> UpdateUser([FromBody] ClientInfo clientInfo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                this.clientsService.UpdateClient(clientInfo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return response;
            }
        }

        [Route("remove")]
        [HttpPost]
        public ActionResult<HttpResponseMessage> RemoveUser([FromBody] ClientInfo clientInfo)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                this.clientsService.RemoveClient(clientInfo);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                response.StatusCode = System.Net.HttpStatusCode.Conflict;
                return response;
            }
        }

        [HttpGet]
        public ActionResult<string> GetPageOfClients(int top, int page)
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetPageOfClients(top, page));
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("client")]
        [HttpGet]
        public ActionResult<string> GetClientByNameAndSurname(string name, string surname)
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetClientInfoByNameSurname(name, surname));
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("all")]
        [HttpGet]
        public ActionResult<string> GetAll()
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetAllClientsInfos());
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("checkzone")]
        [HttpGet]
        public ActionResult<string> GetCheckIfBlackZoneIsNotEmpty()
        {
            try
            {
                var responseContent = JsonConvert.SerializeObject(this.showDbInfoService.GetCheckIfBlackZoneIsNotEmpty());
                return responseContent;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
