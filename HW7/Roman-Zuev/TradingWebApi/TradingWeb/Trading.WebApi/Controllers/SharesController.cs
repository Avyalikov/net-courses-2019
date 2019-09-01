using Microsoft.AspNetCore.Mvc;
using System;
using Trading.Core.Models;
using Trading.Core.Services;

namespace Trading.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly IClientsService clientService;
        private readonly ISharesService sharesService;

        public SharesController(IClientsService clientService, ISharesService sharesService)
        {
            this.clientService = clientService;
            this.sharesService = sharesService;
        }

        [HttpGet]
        public ActionResult<string> GetShares(int clientId)
        {
            try
            {
                string shares = string.Empty;
                foreach (var item in clientService.GetClientSharesById(clientId))
                {
                    shares += $"Shares: {item.Key.SharesType} Price: {item.Key.Price} Quantity: {item.Value}{Environment.NewLine}";
                }
                return shares;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]
        public ActionResult<string> Add([FromBody] SharesEntity sharesToAdd)
        {
            try
            {
                sharesService.Add(sharesToAdd);
                return Ok("Shares successfully added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult<string> Update([FromBody] SharesEntity sharesToAdd)
        {
            try
            {
                sharesService.Update(sharesToAdd);
                return Ok("Shares successfully updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult<string> Remove([FromBody] SharesEntity sharesToRemove)
        {
            try
            {
                sharesService.Remove(sharesToRemove);
                return Ok("Shares successfully removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
