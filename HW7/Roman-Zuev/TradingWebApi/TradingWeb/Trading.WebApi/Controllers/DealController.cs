using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Dto;
using Trading.Core.Services;

namespace Trading.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public DealController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost("[action]")]
        public ActionResult<string> Make([FromBody] TransactionArguments args)
        {
            try
            {
                transactionService.MakeTransaction(args);
                return Ok("Transaction Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
